using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ABT.TestSpace.TestExec;
using ABT.TestSpace.TestExec.AppConfig;
using ABT.TestSpace.TestExec.Logging;
using ABT.TestSpace.TestExec.SCPI_VISA_Instruments;
using ABT.TestSpace.TestExec.Switching.USB_ERB24;
using ABT.TestSpace.UUT_Number.Switching;
using static ABT.TestSpace.TestExec.Switching.RelayForms;

namespace ABT.TestSpace.UUT_Number.Instruments {
    internal static class Switching {
        internal static String ShortsTest() {
            Dictionary<String, String> args = MeasurementAbstract.ArgumentsSplit(((MeasurementCustom)TestExecutor.Only.MeasurementPresent.ClassObject).ArgumentsGet());
            Double threshold = Double.Parse(args["Threshold"]);
            StringBuilder shorts = new StringBuilder(); shorts.AppendLine("Switched Net measured to all others:");
            HashSet<SwitchedNet> MM_P = new HashSet<SwitchedNet>() { Nets.EL, Nets.WG, Nets.P1V2, Nets.P3V3, Nets.P5V0, Nets.P28V0 };
            HashSet<SwitchedNet> MM_N = new HashSet<SwitchedNet>() { Nets.GND };
            MM_N.UnionWith(MM_P);
            SVI.MM.DisConnect();
            Switches.Routes.Set(Nets.MM_N, MM_N, SWITCHED_STATE.CONNECTED);
            Debug.Assert(UE24.Are(UE.B0, new HashSet<R>() {R.C01, R.C03, R.C05, R.C07, R.C09, R.C11}, C.S.NO));
            Debug.Assert(UE24.Are(UE.B0, new Dictionary<R, C.S>() { {R.C01, C.S.NC}, {R.C02, C.S.NO} }));

            TestExecutor.Only.MeasurementPresent.Result = EventCodes.PASS;
            Double Ω;
            MM_34461A.DelaySet(TestExecutor.Only.SVIs[SVIA.MM], MMD.DEFault); MM_34461A.DelayAutoSet(TestExecutor.Only.SVIs[SVIA.MM], true);
            foreach (SwitchedNet sn in MM_P) {
                Switches.Routes.Switch(sn, From: Nets.MM_N, To: Nets.MM_P);
                Ω = MM_34461A.Get(TestExecutor.Only.SVIs[SVIA.MM], PROPERTY.Resistance);
                if (Ω < threshold) TestExecutor.Only.MeasurementPresent.Result = EventCodes.FAIL;
                Switches.Routes.Switch(sn, From: Nets.MM_P, To: Nets.MM_N);
                shorts.AppendLine($"{Logger.SPACES_21} {sn,-11}: {Ω:G2} Ω");
            }
            SVI.MM.DisConnect();
            return shorts.ToString();
        }
    }
}