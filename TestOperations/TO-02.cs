﻿using System;
using System.Diagnostics;
using System.Threading;
using ABT.TestSpace.TestExec;
using ABT.TestSpace.TestExec.AppConfig;
using ABT.TestSpace.TestExec.Processes;
using ABT.TestSpace.TestExec.SCPI_VISA_Instruments;
using ABT.TestSpace.UUT_Number.Instruments;
using ABT.TestSpace.UUT_Number.Switching;

namespace ABT.TestSpace.UUT_Number.TestOperations {
    internal static partial class TestMeasurements {
        // NOTE: Invocable test methods in class TestMeasurements, defined as TestMeasurement IDs in App.config, require signatures like "internal static String MethodName()".
        #region App.config GroupID "TG-02"
        internal static String TM0500() {
            Debug.Assert(TestExecutor.Only.IsOperation(
                OperationID: "TO-02",
                Description: "Test Operation TO-02.",
                Revision: "1.0",
                GroupsIDs: "TG-02"));
            Debug.Assert(TestExecutor.Only.IsGroup(
                GroupID: "TG-02",
                Description: "Test Group TG-02, U1 Erase, Program, Verify & CRC.",
                MeasurementIDs: "TM0500|TM0600|TM0700|TM0800|TM0900",
                Selectable: false,
                CancelNotPassed: true));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0500, U1 Erase.",
                IDPrior: "TM0400",
                IDNext: "TM0600",
                ClassName: MeasurementProcess.ClassName,
                CancelNotPassed: true,
                Arguments: @"ProcessFolder=C:\Program Files\Microchip\MPLABX\v6.15\mplab_platform\mplab_ipe\|ProcessExecutable=ipecmd.exe|ProcessArguments=/P12LF1552 /E|ProcessExpected=0"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            //return PICkit4.Process(PROCESS_METHOD.ExitCode, IC: PICkit4.U1);
            return 0.ToString();
        }

        internal static String TM0600() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-02"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-02"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0600, U1 Program.",
                IDPrior: "TM0500",
                IDNext: "TM0700",
                ClassName: MeasurementProcess.ClassName,
                CancelNotPassed: true,
                Arguments: @"ProcessFolder=C:\Program Files\Microchip\MPLABX\v6.15\mplab_platform\mplab_ipe\|ProcessExecutable=ipecmd.exe|ProcessArguments=/P12LF1552 /M /TPPK4 /FMyFirmwareFile.hex|ProcessExpected=0"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            //return PICkit4.Process(PROCESS_METHOD.ExitCode, IC: PICkit4.U1);
            return 0.ToString();
        }

        internal static String TM0700() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-02"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-02"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0700, U1 Verify.",
                IDPrior: "TM0600",
                IDNext: "TM0800",
                ClassName: MeasurementProcess.ClassName,
                CancelNotPassed: true,
                Arguments: @"ProcessFolder=C:\Program Files\Microchip\MPLABX\v6.15\mplab_platform\mplab_ipe\|ProcessExecutable=ipecmd.exe|ProcessArguments=/P12LF1552 /Y /TPPK4 /FMyFirmwareFile.hex|ProcessExpected=0"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            //return PICkit4.Process(PROCESS_METHOD.ExitCode, IC: PICkit4.U1);
            return 0.ToString();
         }

        internal static String TM0800() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-02"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-02"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0800, U1 Erase, Program & Verify.",
                IDPrior: "TM0700",
                IDNext: "TM0900",
                ClassName: MeasurementProcess.ClassName,
                CancelNotPassed: true,
                Arguments: @"ProcessFolder=C:\Program Files\Microchip\MPLABX\v6.15\mplab_platform\mplab_ipe\|ProcessExecutable=ipecmd.exe|ProcessArguments=/P12LF1552 /E /M /Y /TPPK4 /FMyFirmwareFile.hex|ProcessExpected=0"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            //return PICkit4.Process(PROCESS_METHOD.ExitCode, IC: PICkit4.U1);
            return 0.ToString();
         }

        internal static String TM0900() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-02"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-02"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0900, U1 CRC.",
                IDPrior: "TM0800",
                IDNext: TestExecutive.NONE,
                ClassName: MeasurementProcess.ClassName,
                CancelNotPassed: true,
                Arguments: @"ProcessFolder=C:\Program Files\Microchip\MPLABX\v6.15\mplab_platform\mplab_ipe\|ProcessExecutable=ipecmd.exe|ProcessArguments=/P12LF1552 /TPPK4 /FMyFirmwareFile.hex -G -K|ProcessExpected=ABCD"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            //return PICkit4.Process(PROCESS_METHOD.Redirect, IC: PICkit4.U1);
            return "ABCD";
        }
        #endregion App.config GroupID "TG-02"
    }
}
