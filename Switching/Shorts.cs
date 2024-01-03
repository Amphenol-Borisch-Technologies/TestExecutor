using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ABT.TestSpace.TestExec;
using ABT.TestSpace.TestExec.AppConfig;
using ABT.TestSpace.TestExec.Logging;
using ABT.TestSpace.TestExec.SCPI_VISA_Instruments;
using ABT.TestSpace.TestExec.Switching.USB_ERB24;
using static ABT.TestSpace.TestExec.Switching.RelayForms;
using ABT.TestSpace.UUT_Number.Instruments;

namespace ABT.TestSpace.UUT_Number.Switching {
    internal static partial class Switches {
        internal static String Shorts() {
            Dictionary<String, String> args = MeasurementAbstract.ArgumentsSplit(((MeasurementCustom)TestExecutor.Only.MeasurementPresent.ClassObject).ArgumentsGet());
            Double threshold = Double.Parse(args["Threshold"]);
            StringBuilder shorts = new StringBuilder(); shorts.AppendLine("Switched Net measured to all others:");
            HashSet<SwitchedNet> MM_P = new HashSet<SwitchedNet>() { Nets.EL, Nets.WG, Nets.P1V2, Nets.P3V3, Nets.P5V0, Nets.P28V0 };
            HashSet<SwitchedNet> MM_N = new HashSet<SwitchedNet>() { Nets.GND };
            MM_N.UnionWith(MM_P);
            SVI.MM.DisConnect();
            Routes.Set(Nets.MM_N, MM_N, SWITCHED_STATE.CONNECTED);
            Debug.Assert(UE24.Are(UE.B0, new HashSet<R>() {R.C02, R.C04, R.C06, R.C08, R.C10, R.C12, R.C14, R.C16, R.C18, R.C20, R.C22, R.C24}, C.S.NO));
            Debug.Assert(UE24.Are(UE.B1, new Dictionary<R, C.S>() { {R.C01, C.S.NC}, {R.C02, C.S.NO} }));

            TestExecutor.Only.MeasurementPresent.Result = EventCodes.PASS;
            Double Ω;
            MM_34461A.DelaySet(TestExecutor.Only.SVIs[SVIA.MM], MMD.DEFault); MM_34461A.DelayAutoSet(TestExecutor.Only.SVIs[SVIA.MM], true);
            foreach (SwitchedNet sn in MM_P) {
            // TODO:  Eventually; Nets.P28V0 "shorted" to Nets.GND at E36234A output terminals.
            // Keysight recommends installing an external relay between the E36234A output terminals & its loads,
            // and opening/closing the relay via the E36234A commands .SCPI.OUTPut.RELay.Command(false) & .SCPI.OUTPut.RELay.Command(true);
                if (sn == Nets.P28V0) continue; // TODO:  Eventually; if ever add/control external relay to/the E36234A output terminals, remove this statement.
                Routes.Switch(sn, From: Nets.MM_N, To: Nets.MM_P);
                Ω = MM_34461A.Get(TestExecutor.Only.SVIs[SVIA.MM], PROPERTY.Resistance);
                if (Ω < threshold) TestExecutor.Only.MeasurementPresent.Result = EventCodes.FAIL;
                Routes.Switch(sn, From: Nets.MM_P, To: Nets.MM_N);
                shorts.AppendLine($"{Logger.SPACES_21} {sn,-11}: {Ω:G2} Ω");
            }
            Debug.Assert(!UE24.Are(UE.B1, C.S.NC));
            SVI.MM.DisConnect();
            return shorts.ToString();
        }
    }
}