using System;
using ABT.TestSpace.TestExec.Switching.USB_ERB24;

namespace ABT.TestSpace.UUT_Number.Switching {
    /// <summary>
    /// SN correlates Customer UUT nets to ABT test system switched nets:
    /// <para>
    /// - SN exclusively correlates switched nets connected via relays, which can be temporarily connected/disconnected as desired.
    /// - SN specifically excludes unswitched nets connected via permanent circuitry; signal conditioners, wire harness continuities, etc.
    /// SN cross-references Customer UUT input stimuli & output signals switched into ABT test system inputs/outputs.
    /// - SN name is Customer UUT's net name, SN value is correlated ABT test system switched net name.
    /// - If ABT test system has multiple names for nets, prefer utilizing the switched net name over permanently connected name.
    /// 
    /// So, Customer UUT power supplies might be meaningfully correlated to ABT test system fixturing & instrumentation as follows:
    ///     internal static class SN {
    ///         internal static readonly SwitchedNet P3V3    = new SwitchedNet(nameof(P3V3), Alias: "3.3V"));
    ///         internal static readonly SwitchedNet P5V     = new SwitchedNet(nameof(P5V),  Alias: "5V"));
    ///         internal static readonly SwitchedNet P12V    = new SwitchedNet(nameof(P12V), Alias: "+12V"));
    ///         internal static readonly SwitchedNet N12V    = new SwitchedNet(nameof(N12V), Alias: "-12V"));
    ///     }
    /// </para>
    /// </summary>
    internal static class Nets {
        internal static readonly SwitchedNet Ø     = new SwitchedNet(ID: nameof(Ø),      Alias: String.Empty);
        internal static readonly SwitchedNet EL    = new SwitchedNet(ID: nameof(EL),     Alias: "Load");
        internal static readonly SwitchedNet WG    = new SwitchedNet(ID: nameof(WG),     Alias: "CLK");
        internal static readonly SwitchedNet MM_I  = new SwitchedNet(ID: nameof(MM_I),   Alias: "MM I");
        internal static readonly SwitchedNet MM_N  = new SwitchedNet(ID: nameof(MM_N),   Alias: "MM -");
        internal static readonly SwitchedNet MM_P  = new SwitchedNet(ID: nameof(MM_P),   Alias: "MM +");
        internal static readonly SwitchedNet P1V2  = new SwitchedNet(ID: nameof(P1V2),   Alias: "+1.2V DC");
        internal static readonly SwitchedNet P3V3  = new SwitchedNet(ID: nameof(P3V3),   Alias: "+3.3V DC");
        internal static readonly SwitchedNet P5V0  = new SwitchedNet(ID: nameof(P5V0),   Alias: "+5 VDC");
        internal static readonly SwitchedNet P28V0 = new SwitchedNet(ID: nameof(P28V0),  Alias: "+28 VDC");
        internal static readonly SwitchedNet GND   = new SwitchedNet(ID: nameof(GND),    Alias: "GND");
    }
}