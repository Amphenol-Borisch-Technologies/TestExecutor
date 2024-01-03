using System.Collections.Generic;
using ABT.TestSpace.TestExec.Switching.USB_ERB24;

namespace ABT.TestSpace.UUT_Number.Switching {
    internal static partial class Switches {
        internal static Relays Relays = new Relays(new HashSet<Relay>() {
                { new Relay(UE.B0, R.C01, C: Nets.MM_P,  NC: Nets.Ø,  NO: Nets.EL) },   
                { new Relay(UE.B0, R.C02, C: Nets.MM_N,  NC: Nets.Ø,  NO: Nets.EL) },
                { new Relay(UE.B0, R.C03, C: Nets.MM_P,  NC: Nets.Ø,  NO: Nets.WG) },
                { new Relay(UE.B0, R.C04, C: Nets.MM_N,  NC: Nets.Ø,  NO: Nets.WG) },
                { new Relay(UE.B0, R.C05, C: Nets.MM_P,  NC: Nets.Ø,  NO: Nets.P1V2) },
                { new Relay(UE.B0, R.C06, C: Nets.MM_N,  NC: Nets.Ø,  NO: Nets.P1V2) },
                { new Relay(UE.B0, R.C07, C: Nets.MM_P,  NC: Nets.Ø,  NO: Nets.P3V3) },
                { new Relay(UE.B0, R.C08, C: Nets.MM_N,  NC: Nets.Ø,  NO: Nets.P3V3) },
                { new Relay(UE.B0, R.C09, C: Nets.MM_P,  NC: Nets.Ø,  NO: Nets.P5V0) },
                { new Relay(UE.B0, R.C10, C: Nets.MM_N,  NC: Nets.Ø,  NO: Nets.P5V0) },
                { new Relay(UE.B0, R.C11, C: Nets.MM_P,  NC: Nets.Ø,  NO: Nets.P28V0) },
                { new Relay(UE.B0, R.C12, C: Nets.MM_N,  NC: Nets.Ø,  NO: Nets.P28V0) },
                { new Relay(UE.B0, R.C13, C: Nets.MM_P,  NC: Nets.Ø,  NO: Nets.GND) },
                { new Relay(UE.B0, R.C14, C: Nets.MM_N,  NC: Nets.Ø,  NO: Nets.GND) }
        });
    }
}
