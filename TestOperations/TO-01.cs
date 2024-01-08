using System;
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
        #region App.config GroupID "TG-01"
        internal static String TM0100() {
            Debug.Assert(TestExecutor.Only.IsOperation(
                OperationID: "TO-01",
                Description: "Test Operation TO-01.",
                Revision: "1.0",
                GroupsIDs: "TG-01"));
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
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Honoring Cancellation request.");
                throw new CancellationException("Proactive Cancellation requested.");
            }
            TestExecutor.Only.LogMessage(Label: "Note", Message: "Demonstrate a Passing test run.");
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
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Honoring Cancellation request.");
                throw new CancellationException("Proactive Cancellation requested.");
            }
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
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Honoring Cancellation request.");
                throw new CancellationException("Proactive Cancellation requested.");
            }
            return 3.3.ToString();
        }

        internal static String TM0400() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-01"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-01"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0400, P5V0 ±5%.",
                IDPrior: "TM0300",
                IDNext: TestExecutive.NONE,
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
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Honoring Cancellation request.");
                throw new CancellationException("Proactive Cancellation requested.");
            }
            return 5.ToString();
        }
        #endregion App.config GroupID "TG-01"
    }
}

