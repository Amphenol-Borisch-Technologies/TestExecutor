using System;
using System.Diagnostics;
using ABT.TestSpace.TestExec;
using ABT.TestSpace.TestExec.AppConfig;
using ABT.TestSpace.TestExec.Processes;
using ABT.TestSpace.TestExec.SCPI_VISA_Instruments;
using ABT.TestSpace.UUT_Number.Instruments;
using ABT.TestSpace.UUT_Number.Switching;

namespace ABT.TestSpace.UUT_Number.TestOperations {
    internal static partial class TestMeasurements {
        // NOTE: Invocable test methods in class TestMeasurements, defined as TestMeasurement IDs in App.config, require signatures like "internal static String MethodName()".
        #region App.config GroupID "TG-01"
        internal static String TM0100() {
            Debug.Assert(TestExecutor.Only.IsOperation(
                OperationID: "TO-01",
                Description: "Test Operation TO-01.",
                Revision: "1.0",
                GroupsIDs: "TG-01|TG-02"));
            Debug.Assert(TestExecutor.Only.IsGroup(
                GroupID: "TG-01",
                Description: "Test Group TG-01, Shorts & Power.",
                MeasurementIDs: "TM0100|TM0200|TM0300|TM0400",
                Selectable: false,
                CancelNotPassed: true));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0100, Shorts.",
                IDPrior: TestExecutive.NONE,
                IDNext: "TM0200",
                ClassName: MeasurementNumeric.ClassName,
                CancelNotPassed: true,
                Arguments: "High=∞|Low=10|SI_Units=ohms|SI_Units_Modifier=NotApplicable"));
            // Debug.Assert(TestExecutor.Only.Initialized());
            // return Switches.Shorts();
            return Double.PositiveInfinity.ToString();
        }

        internal static String TM0200() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-01"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-01"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0200, P2V5 ±5%.",
                IDPrior: "TM0100",
                IDNext: "TM0300",
                ClassName: MeasurementNumeric.ClassName,
                CancelNotPassed: true,
                Arguments: "High=2.625|Low=2.375|SI_Units=volts|SI_Units_Modifier=DC"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.off,
            //    P3V3: STATE.off,
            //    P5V0: STATE.off,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            //SVI.PS_Fixed.Set(SVIA.P2V5, STATE.ON);
            //return SVI.PS_Fixed.Get(SVIA.P2V5, PS_DC.Volts).ToString();
            return 2.5.ToString();
        }

        internal static String TM0300() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-01"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-01"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0300, P3V3 ±5%.",
                IDPrior: "TM0200",
                IDNext: "TM0400",
                ClassName: MeasurementNumeric.ClassName,
                CancelNotPassed: true,
                Arguments: "High=3.465|Low=3.135|SI_Units=volts|SI_Units_Modifier=DC"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.off,
            //    P5V0: STATE.off,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            //SVI.PS_Fixed.Set(SVIA.P3V3, STATE.ON);
            //return SVI.PS_Fixed.Get(SVIA.P3V3, PS_DC.Volts).ToString();
            return 3.3.ToString();
        }

        internal static String TM0400() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-01"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-01"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0400, P5V0 ±5%.",
                IDPrior: "TM0300",
                IDNext: "TM0500",
                ClassName: MeasurementNumeric.ClassName,
                CancelNotPassed: true,
                Arguments: "High=5.25|Low=4.75|SI_Units=volts|SI_Units_Modifier=DC"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.off,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            //SVI.PS_Fixed.Set(SVIA.P5V0, STATE.ON);
            //return SVI.PS_Fixed.Get(SVIA.P5V0, PS_DC.Volts).ToString();
            return 5.ToString();
        }
        #endregion App.config GroupID "TG-01"

        #region App.config GroupID "TG-02"
        internal static String TM0500() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-01"));
            Debug.Assert(TestExecutor.Only.IsGroup(
                GroupID: "TG-02",
                Description: "Test Group TG-02, U1 Erase, Program, Verify & CRC.",
                MeasurementIDs: "TM0500|TM0600|TM0700|TM0800",
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
            Debug.Assert(TestExecutor.Only.IsOperation("TO-01"));
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
            Debug.Assert(TestExecutor.Only.IsOperation("TO-01"));
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
            Debug.Assert(TestExecutor.Only.IsOperation("TO-01"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-02"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0800, U1 CRC.",
                IDPrior: "TM0700",
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

