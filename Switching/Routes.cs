using System;
using System.Collections.Generic;
using ABT.TestSpace.TestExec.Switching.USB_ERB24;
using static ABT.TestSpace.TestExec.Switching.RelayForms;

namespace ABT.TestSpace.UUT_Number.Switching {
    internal static partial class Switches {
        // TODO:  Eventually; add Depth First Search (DFS) algorithm to create SwitchedRoutes object programmatically.  https://en.wikipedia.org/wiki/Depth-first_search.
        // - Declare SwitchedNets, declare Relays.
        // - DFS algorithm maps the relay wiring, verifying/validating it for typographic errors first, similar to AppConfigTests' validations.
        // - Test developer invokes SwitchedRoutes methods Set() to switch relays on/off, connecting/disconnecting SwitchedNets to
        //   test system instrumentation.
        // - Connectable() methods useful for Debug.Assert() verifications.
        internal static SwitchedRoutes Routes = new SwitchedRoutes(new Dictionary<SwitchedRoute, HashSet<State>>() {
                { new SwitchedRoute(Tuple.Create(Nets.MM_P,  Nets.EL)),    new HashSet<State>() { new State(UE.B0, R.C01, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_N,  Nets.EL)),    new HashSet<State>() { new State(UE.B0, R.C02, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_P,  Nets.WG)),    new HashSet<State>() { new State(UE.B0, R.C03, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_N,  Nets.WG)),    new HashSet<State>() { new State(UE.B0, R.C04, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_P,  Nets.P1V2)),  new HashSet<State>() { new State(UE.B0, R.C05, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_N,  Nets.P1V2)),  new HashSet<State>() { new State(UE.B0, R.C06, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_P,  Nets.P3V3)),  new HashSet<State>() { new State(UE.B0, R.C07, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_N,  Nets.P3V3)),  new HashSet<State>() { new State(UE.B0, R.C08, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_P,  Nets.P5V0)),  new HashSet<State>() { new State(UE.B0, R.C09, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_N,  Nets.P5V0)),  new HashSet<State>() { new State(UE.B0, R.C10, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_P,  Nets.P28V0)), new HashSet<State>() { new State(UE.B0, R.C11, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_N,  Nets.P28V0)), new HashSet<State>() { new State(UE.B0, R.C12, C.S.NO) } },
                { new SwitchedRoute(Tuple.Create(Nets.MM_P,  Nets.GND)),   new HashSet<State>() { new State(UE.B0, R.C13, C.S.NO) } }
        });
    }
}
