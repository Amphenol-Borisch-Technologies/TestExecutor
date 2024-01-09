using System;
using System.Collections.Generic;
using System.Threading;
using ABT.TestSpace.TestExec;
using ABT.TestSpace.TestExec.SCPI_VISA_Instruments;
using ABT.TestSpace.TestExec.Switching.USB_ERB24;
using ABT.TestSpace.UUT_Number.Switching;
using static ABT.TestSpace.TestExec.SCPI_VISA_Instruments.SCPI_VISA_Instrument;
using static ABT.TestSpace.TestExec.Switching.RelayForms;

namespace ABT.TestSpace.UUT_Number.Instruments {
    internal enum FREQUENCY { off, KHz1E1, KHz1E2, KHz1E3 }
    internal enum LOAD_CURRENT { off, A0_015, A3_5, A4_375, A4_675, A6_0, A7_0, A8_75, A11_67, A12_0, A14_5 }
    internal enum STIMULUS { off, V1_A0_2, V12_5_A0_2, V18_A0_2, V18_W15, V18_W100, V18_W120, V28_W15, V28_W52_5, V28_W100, V28_W120, V36_W15, V36_W100 }
    internal enum SVIs { EL, WG, P2V5, P3V3, P5V0, VIN }

    internal static class SVI {
        internal static Boolean Are(STATE P2V5, STATE P3V3, STATE P5V0, STIMULUS VIN, LOAD_CURRENT EL, FREQUENCY WG) {
            return PS_Fixed.Is(SVIA.P2V5, P2V5)
                && PS_Fixed.Is(SVIA.P3V3, P3V3)
                && PS_Fixed.Is(SVIA.P5V0, P5V0)
                && SVI.VIN.Is(VIN)
                && SVI.EL.Is(EL)
                && SVI.WG.Is(WG);
        }

        internal static void Set(STATE P2V5, STATE P3V3, STATE P5V0, STIMULUS VIN, LOAD_CURRENT EL, FREQUENCY WG) {
            if (EL != LOAD_CURRENT.off) SVI.EL.Set(EL); // Apply Electronic Load first, remove last.
            PS_Fixed.Set(SVIA.P2V5, P2V5);
            PS_Fixed.Set(SVIA.P3V3, P3V3);
            PS_Fixed.Set(SVIA.P5V0, P5V0);
            SVI.VIN.Set(VIN);
            SVI.WG.Set(WG);
            SVI.EL.Set(EL);
        }

        internal static Boolean StimuliAreOff() {
            return Are(
                P2V5: STATE.off,
                P3V3: STATE.off,
                P5V0: STATE.off,
                VIN: STIMULUS.off,
                EL: LOAD_CURRENT.off,
                WG: FREQUENCY.off);
        }

        internal static void StimuliSetOff() {
            Set(
                P2V5: STATE.off,
                P3V3: STATE.off,
                P5V0: STATE.off,
                VIN: STIMULUS.off,
                EL: LOAD_CURRENT.off,
                WG: FREQUENCY.off);
        }

        internal static class MM {
            internal static Double GetPowerBusVDC(SwitchedNet switchedNet) {
                DisConnect();
                Switches.Routes.Set(Nets.MM_P, switchedNet, SWITCHED_STATE.CONNECTED);
                Switches.Routes.Set(Nets.MM_N, Nets.GND, SWITCHED_STATE.CONNECTED);
                Double vdc = MM_34461A.Get(TestExecutor.Only.SVIs[SVIA.MM], PROPERTY.VoltageDC);
                DisConnect();
                return vdc;
            }

            internal static void DisConnect() { UE24.Set(UE.B0, C.S.NC); }
        }

        internal static class PS_Fixed {
            internal static Boolean Is(Alias alias, STATE state) { return SCPI99.Is(TestExecutor.Only.SVIs[alias], state); }

            internal static Double Get(Alias alias, PS_DC dc) { return PS_E3610xB.Get(TestExecutor.Only.SVIs[alias], dc); }

            internal static void Set(Alias alias, STATE state) {
                if (Is(alias, state)) return; // Don't reapply unnecessarily; might cause transient stimulus during reapplication.
                if (state == STATE.off) SCPI99.Set(TestExecutor.Only.SVIs[alias], STATE.off);
                else {
                    Double voltsDC;
                    switch(alias) {
                        case Alias a when a == SVIA.P2V5:
                            voltsDC = 2.5;
                            break;
                        case Alias a when a == SVIA.P3V3:
                            voltsDC = 3.3;
                            break;
                        case Alias a when a == SVIA.P5V0:
                            voltsDC = 5.0;
                            break;
                        default:
                            throw new NotImplementedException($"Unsupported SVIA Alias {alias}.");
                    }
                    PS_E3610xB.Set(TestExecutor.Only.SVIs[alias], STATE.ON, VoltsDC: voltsDC, AmpsDC: 1.0, SENSE_MODE.EXTernal, DelaySecondsCurrentProtection: 0.5, DelaySecondsSettling: 0.5);
                }
            }
        }

        internal static class VIN {
            internal static Boolean Is(STIMULUS Stimulus) {
                if (Stimulus == STIMULUS.off) return PS_E36234A.Is(TestExecutor.Only.SVIs[SVIA.VIN], STATE.off, CHANNEL.C1);
                return PS_E36234A.Is(TestExecutor.Only.SVIs[SVIA.VIN], STATE.ON, CHANNEL.C1)
                    && PS_E36234A.VoltageAmplitudeIs(TestExecutor.Only.SVIs[SVIA.VIN], Stimuli[Stimulus].VoltsDC, Delta: 0.01, CHANNEL.C1)
                    && PS_E36234A.CurrentAmplitudeIs(TestExecutor.Only.SVIs[SVIA.VIN], Stimuli[Stimulus].AmpsDC, Delta: 0.01, CHANNEL.C1)
                    && PS_E36234A.SlewRatesAre(TestExecutor.Only.SVIs[SVIA.VIN], Stimuli[Stimulus].SlewRateRising, Stimuli[Stimulus].SlewRateFalling, CHANNEL.C1);
            }

            internal static Double Get(PS_DC UPS) { return PS_E36234A.Get(TestExecutor.Only.SVIs[SVIA.VIN], UPS, CHANNEL.C1, SENSE_MODE.EXTernal); }

            internal static void Set(STIMULUS Stimulus) {
                if (Is(Stimulus)) return; // Don't reapply unnecessarily; might interrupt stimulus during reapplication.
                if (Stimulus == STIMULUS.off) {
                    SCPI99.Set(TestExecutor.Only.SVIs[SVIA.VIN], STATE.off);
                    Thread.Sleep(millisecondsTimeout: 500);
                } else {
                    PS_E36234A.SlewRatesSet(TestExecutor.Only.SVIs[SVIA.VIN], Stimuli[Stimulus].SlewRateRising, Stimuli[Stimulus].SlewRateFalling, CHANNEL.C1);
                    PS_E36234A.Set(TestExecutor.Only.SVIs[SVIA.VIN], STATE.ON, Stimuli[Stimulus].VoltsDC, Stimuli[Stimulus].AmpsDC, VoltageProtectionAmplitude: 50, CHANNEL.C1, SENSE_MODE.EXTernal, DelaySecondsCurrentProtection: 0.5, DelaySecondsSettling: 0.5);
                }
            }

            internal static void Set(PS_DC DC, Double Amplitude) { PS_E36234A.Set(TestExecutor.Only.SVIs[SVIA.VIN], DC, Amplitude, CHANNEL.C1, SENSE_MODE.EXTernal); }

            internal static readonly Dictionary<STIMULUS, Stimulus> Stimuli = new Dictionary<STIMULUS, Stimulus> {
                { STIMULUS.off,        new Stimulus(voltsDC: 0,    ampsDC: 0,      slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V1_A0_2,    new Stimulus(voltsDC: 1.0,  ampsDC: 0.20,   slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V12_5_A0_2, new Stimulus(voltsDC: 12.5, ampsDC: 0.20,   slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V18_A0_2,   new Stimulus(voltsDC: 18.0, ampsDC: 0.20,   slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V18_W15,    new Stimulus(voltsDC: 18.0, ampsDC: 0.833,  slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V18_W100,   new Stimulus(voltsDC: 18.0, ampsDC: 5.556,  slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V18_W120,   new Stimulus(voltsDC: 18.0, ampsDC: 6.667,  slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V28_W15,    new Stimulus(voltsDC: 28.0, ampsDC: 0.536,  slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V28_W52_5,  new Stimulus(voltsDC: 28.0, ampsDC: 1.875,  slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V28_W100,   new Stimulus(voltsDC: 28.0, ampsDC: 3.571,  slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V28_W120,   new Stimulus(voltsDC: 28.0, ampsDC: 4.286,  slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V36_W15,    new Stimulus(voltsDC: 36.0, ampsDC: 0.417,  slewRateRising: 1000, slewRateFalling: 1000) },
                { STIMULUS.V36_W100,   new Stimulus(voltsDC: 36.0, ampsDC: 2.778,  slewRateRising: 1000, slewRateFalling: 1000) }
            };

            internal class Stimulus {
                internal readonly Double VoltsDC;
                internal readonly Double AmpsDC;
                internal readonly Double SlewRateRising;
                internal readonly Double SlewRateFalling;
                internal Stimulus(Double voltsDC, Double ampsDC, Double slewRateRising, Double slewRateFalling) { VoltsDC = voltsDC; AmpsDC = ampsDC; SlewRateRising = slewRateRising; SlewRateFalling = slewRateFalling; }
            }
        }

        internal static class EL {
            private static readonly Dictionary<LOAD_CURRENT, LoadCurrent> LoadCurrents = new Dictionary<LOAD_CURRENT, LoadCurrent> {
                { LOAD_CURRENT.off,     new LoadCurrent(AmpsDC: 0,     SlewRateRising: 5000, SlewRateFalling: 5000) },
                { LOAD_CURRENT.A0_015,  new LoadCurrent(AmpsDC: 0.015, SlewRateRising: 5000, SlewRateFalling: 5000) },
                { LOAD_CURRENT.A3_5,    new LoadCurrent(AmpsDC: 3.5,   SlewRateRising: 5000, SlewRateFalling: 5000) },
                { LOAD_CURRENT.A4_375,  new LoadCurrent(AmpsDC: 4.375, SlewRateRising: 5000, SlewRateFalling: 5000) },
                { LOAD_CURRENT.A4_675,  new LoadCurrent(AmpsDC: 4.675, SlewRateRising: 5000, SlewRateFalling: 5000) },
                { LOAD_CURRENT.A6_0,    new LoadCurrent(AmpsDC: 6.0,   SlewRateRising: 5000, SlewRateFalling: 5000) },
                { LOAD_CURRENT.A7_0,    new LoadCurrent(AmpsDC: 7.0,   SlewRateRising: 5000, SlewRateFalling: 5000) },
                { LOAD_CURRENT.A8_75,   new LoadCurrent(AmpsDC: 8.75,  SlewRateRising: 5000, SlewRateFalling: 5000) },
                { LOAD_CURRENT.A11_67,  new LoadCurrent(AmpsDC: 11.67, SlewRateRising: 5000, SlewRateFalling: 5000) },
                { LOAD_CURRENT.A12_0,   new LoadCurrent(AmpsDC: 12.0,  SlewRateRising: 5000, SlewRateFalling: 5000) },
                { LOAD_CURRENT.A14_5,   new LoadCurrent(AmpsDC: 14.5,  SlewRateRising: 5000, SlewRateFalling: 5000) }
            };

            internal static Boolean Is(LOAD_CURRENT Load) {
                if (Load == LOAD_CURRENT.off) return SCPI99.Is(TestExecutor.Only.SVIs[SVIA.EL], STATE.off);
                return EL_34143A.Is(TestExecutor.Only.SVIs[SVIA.EL], STATE.ON)
                    && EL_34143A.Is(TestExecutor.Only.SVIs[SVIA.EL], LoadCurrents[Load].AmpsDC, LOAD_MODE.CURR)
                    && EL_34143A.SlewRatesAre(TestExecutor.Only.SVIs[SVIA.EL], LoadCurrents[Load].SlewRateRising, LoadCurrents[Load].SlewRateFalling, LOAD_MODE.CURR);
            }

            internal static Double Get(LOAD_MEASURE LoadMeasure) { return EL_34143A.Get(TestExecutor.Only.SVIs[SVIA.EL], LoadMeasure, SENSE_MODE.EXTernal); }

            internal static void Set(LOAD_CURRENT Load) {
                if (Is(Load)) return; // Don't reapply unnecessarily; might interrupt loading during reapplication.
                if (Load == LOAD_CURRENT.off) EL_34143A.Set(TestExecutor.Only.SVIs[SVIA.EL], STATE.off);
                else {
                    EL_34143A.SlewRatesSet(TestExecutor.Only.SVIs[SVIA.EL], LoadCurrents[Load].SlewRateRising, LoadCurrents[Load].SlewRateFalling, LOAD_MODE.CURR);
                    EL_34143A.Set(TestExecutor.Only.SVIs[SVIA.EL], STATE.ON, LoadCurrents[Load].AmpsDC, LOAD_MODE.CURR, SENSE_MODE.EXTernal);
                }
            }
            
            internal class LoadCurrent {
                internal readonly Double AmpsDC;
                internal readonly Double SlewRateRising;
                internal readonly Double SlewRateFalling;
                internal LoadCurrent(Double AmpsDC, Double SlewRateRising, Double SlewRateFalling) {
                    this.AmpsDC = AmpsDC;
                    this.SlewRateRising = SlewRateRising;
                    this.SlewRateFalling = SlewRateFalling;
                }
            }
        }

        internal static class WG {
            private static readonly Dictionary<FREQUENCY, Double> Frequencies = new Dictionary<FREQUENCY, Double> {
                { FREQUENCY.off, 0 },
                { FREQUENCY.KHz1E1, 1E1 },
                { FREQUENCY.KHz1E2, 1E2 },
                { FREQUENCY.KHz1E3, 1E3 }
            };

            internal static Boolean Is(FREQUENCY Hertz) {
                if (Hertz == FREQUENCY.off) return WG_33509B.Is(TestExecutor.Only.SVIs[SVIA.WG], STATE.off);
                return WG_33509B.Is(TestExecutor.Only.SVIs[SVIA.WG], STATE.ON) && WaveformIs(Hertz);
            }

            internal static void Set(FREQUENCY Hertz) {
                if (Is(Hertz)) return; // Don't reapply unnecessarily; might cause transient stimulus during reapplication.
                if (Hertz == FREQUENCY.off) SCPI99.Set(TestExecutor.Only.SVIs[SVIA.WG], STATE.off);
                else WG_33509B.WaveformSquareApply(TestExecutor.Only.SVIs[SVIA.WG], Frequencies[Hertz], V_High: 3.3, V_Offset: 1.65);
            }

            private static Boolean WaveformIs(FREQUENCY Hertz) {
                String Waveform = WG_33509B.WaveformGet(TestExecutor.Only.SVIs[SVIA.WG]);
                switch (Hertz) {
                    case FREQUENCY.KHz1E1:  return Waveform.Equals("SQU +1.000000000000000E+01,+3.3000000000000E+00,+1.6500000000000E+00");
                    case FREQUENCY.KHz1E2:  return Waveform.Equals("SQU +1.000000000000000E+02,+3.3000000000000E+00,+1.6500000000000E+00");
                    case FREQUENCY.KHz1E3:  return Waveform.Equals("SQU +1.000000000000000E+03,+3.3000000000000E+00,+1.6500000000000E+00");
                    default:                throw new NotImplementedException(TestExecutive.NotImplementedMessageEnum(typeof(FREQUENCY)));
                }
            }
        }
    }
}
