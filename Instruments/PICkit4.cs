using System;
using ABT.TestSpace.TestExec;
using ABT.TestSpace.TestExec.Processes;
using ABT.TestSpace.TestExec.SCPI_VISA_Instruments;

namespace ABT.TestSpace.UUT_Number.Instruments {
    internal static class PICkit4 {
        internal static String Process(PROCESS_METHOD process_method, Action IC) {
            IC();
            String process_result;
            switch (process_method) {
                case PROCESS_METHOD.ExitCode:  process_result = ProcessExternal.ExitCode(TestExecutor.Only.MeasurementPresent);                 break;
                case PROCESS_METHOD.Redirect:  process_result = ProcessExternal.Redirect(TestExecutor.Only.MeasurementPresent).StandardOutput;  break;
                default:                       throw new NotImplementedException(TestExecutive.NotImplementedMessageEnum(typeof(PROCESS_METHOD)));
            }
            return process_result;
        }

        internal static String MPLAB_IPE_Launch(PROCESS_METHOD process_method, Action IC) {
            IC();
            String process_result;
            switch (process_method) {
                case PROCESS_METHOD.ExitCode:  process_result = ProcessExternal.ExitCode(TestExecutor.Only.MeasurementPresent);                 break;
                case PROCESS_METHOD.Redirect:  process_result = ProcessExternal.Redirect(TestExecutor.Only.MeasurementPresent).StandardOutput;  break;
                default:                       throw new NotImplementedException(TestExecutive.NotImplementedMessageEnum(typeof(PROCESS_METHOD)));
            }
            return process_result;
        }

        internal static void U1() {
            SVI.Set(
                P2V5: STATE.off,
                P3V3: STATE.ON,
                P5V0: STATE.off,
                VIN: STIMULUS.off,
                EL: LOAD_CURRENT.A0_015,
                WG: FREQUENCY.off);
        }
        // NOTE:  Can have multiple ICs to erase/program/verify/crc, each having it's own custom powerup/initialization.
    }
}
