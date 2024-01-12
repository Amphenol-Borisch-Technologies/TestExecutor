using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;
using ABT.TestSpace.TestExec;
using ABT.TestSpace.TestExec.AppConfig;
using ABT.TestSpace.TestExec.Logging;
using ABT.TestSpace.TestExec.SCPI_VISA_Instruments;
using ABT.TestSpace.UUT_Number.Instruments;

namespace ABT.TestSpace.UUT_Number.TestOperations {
    internal static partial class TestMeasurements {
        // NOTE:  Invocable test methods in this class, defined as TestMeasurement IDs in App.Config, require signatures like "internal static String MethodName()".
        #region App.config GroupID "DG-01"
        internal static String DM01() {
            Debug.Assert(!TestExecutor.Only.ConfigTest.IsOperation);
            Debug.Assert(TestExecutor.Only.IsGroup(
                GroupID: "DG-01",
                Description: "Test Group DG-01, Debug CCA.",
                MeasurementIDs: "DM01|DM02|DM03|DM04",
                Selectable: true,
                CancelNotPassed: true));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Debug Measurement DM01.",
                IDPrior: TestExecutive.NONE,
                IDNext: "DM02",
                ClassName: MeasurementCustom.ClassName,
                CancelNotPassed: true,
                Arguments: "NotApplicable"));
            //Debug.Assert(TestExecutor.Only.Initialized());
            //SVI.Set(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off);
            // TODO:  Add measurement code here, return String result.
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Honoring Cancellation request, execution terminated before TestMeasurement completes.");
                throw new CancellationException("Proactive Cancellation occurring.");
            }
            TestExecutor.Only.LogMessage(Label: "Note", Message: "MeasurementCustom requires we explicitly set TestExecutor.Only.MeasurementPresent.Result to resulting EventCode.");
            TestExecutor.Only.MeasurementPresent.Result = EventCodes.PASS;
            TestExecutor.Only.LogMessage(Label: "Note", Message: "MeasurementCustom requires we return a formatted Value.");
            TestExecutor.Only.LogMessage(Label: "Note", Message: "MeasurementCustom permits entirely custom input arguments, but also requires entirely custom Results & return Values.");
            return Logger.FormatMessage(Label: "Actual", Message: "0");
        }

        internal static String DM02() {
            Debug.Assert(!TestExecutor.Only.ConfigTest.IsOperation);
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "DG-01"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Debug Measurement DM02.",
                IDPrior: "DM01",
                IDNext: "DM03",
                ClassName: MeasurementNumeric.ClassName,
                CancelNotPassed: true,
                Arguments: "High=0|Low=-∞|SI_Units=volts|SI_Units_Modifier=DC"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            // TODO:  Add measurement code here, return String result.
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Honoring Cancellation request, execution terminated before TestMeasurement completes.");
                throw new CancellationException("Proactive Cancellation occurring.");
            }
            DialogResult dr = MessageBox.Show("Click Yes to demonstrate Failing TestMeasurement, No to skip.", "Demonstrate Failing TestMeasurement?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Deliberately failing TestMeasurement.  Test result will be 'Cancel', and DM03 won't execute.");
            return Double.PositiveInfinity.ToString();
            }
            TestExecutor.Only.LogMessage(Label: "Note", Message: "MeasurementNumeric allows -∞ for a low limit, to disable the low limit.  Handy for Shorts testing.");
            return Double.NegativeInfinity.ToString();
        }

        internal static String DM03() {
            Debug.Assert(!TestExecutor.Only.ConfigTest.IsOperation);
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "DG-01"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Debug Measurement DM03.",
                IDPrior: "DM02",
                IDNext: "DM04",
                ClassName: MeasurementNumeric.ClassName,
                CancelNotPassed: true,
                Arguments: "High=∞|Low=0|SI_Units=volts|SI_Units_Modifier=DC"));
            // NOTE:  CancelNotPassed: true will cause DG-01 TestGroup to cancel.
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            // TODO:  Add measurement code here, return String result.
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Honoring Cancellation request, execution terminated before TestMeasurement completes.");
                throw new CancellationException("Proactive Cancellation occurring.");
            }
            DialogResult dr = MessageBox.Show("Click Yes to demonstrate Cancellation Exception, No to skip.", "Demonstrate Cancellation Exception?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Deliberately throwing a CancellationException.  Test result will be 'Cancel', and DM04 won't execute.");
                throw new CancellationException("Cancellation Example.");
            }
            TestExecutor.Only.LogMessage(Label: "Note", Message: "MeasurementNumeric allows ∞ for a high limit, to disable the high limit.  Handy for Opens testing.");
            return Double.PositiveInfinity.ToString();
        }

        internal static String DM04() {
            Debug.Assert(!TestExecutor.Only.ConfigTest.IsOperation);
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "DG-01"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Debug Measurement DM04.",
                IDPrior: "DM03",
                IDNext: TestExecutive.NONE,
                ClassName: MeasurementTextual.ClassName,
                CancelNotPassed: true,
                Arguments: "Text=Sample Text String."));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            // TODO:  Add measurement code here, return String result.
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Honoring Cancellation request, execution terminated before TestMeasurement completes.");
                throw new CancellationException("Proactive Cancellation occurring.");
            }
            TestExecutor.Only.LogMessage(Label: "Note", Message: "MeasurementTextual useful for checking text strings, which are case sensitive.");
            TestExecutor.Only.LogMessage(Label: "Note", Message: "Test result will be 'Fail', because test execution won't cancel midway due to a failing TestMeasurement & the TestGroup/TestMeasurement's CancelNotPassed flags.");
            return "Sample Text string.";
        }
        #endregion App.config GroupID "DG-01"

        #region App.config GroupID "DG-02"
        internal static String DM05() {
            Debug.Assert(!TestExecutor.Only.ConfigTest.IsOperation);
            Debug.Assert(TestExecutor.Only.IsGroup(
                GroupID: "DG-02",
                Description: "Test Group DG-02, Debug CCA.",
                MeasurementIDs: "DM05|DM06|DM07|DM08",
                Selectable: true,
                CancelNotPassed: false));
            // NOTE:  CancelNotPassed: true will cause DG-01 TestGroup to not cancel, unless overridden by a TestMeasurement's CancelNotPassed.
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Debug Measurement DM05.",
                IDPrior: TestExecutive.NONE,
                IDNext: "DM06",
                ClassName: MeasurementCustom.ClassName,
                CancelNotPassed: false,
                Arguments: "NotApplicable"));
            //Debug.Assert(TestExecutor.Only.Initialized());
            //SVI.Set(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off);
            // TODO:  Add measurement code here, return String result.
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Not honoring Cancellation request, thus simulating a reactive cancellation, execution terminates after TestMeasurement completes.");
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Reactive Cancellation occurring.");
            }
            TestExecutor.Only.LogMessage(Label: "Note", Message: "Demonstrating both TestGroup & TestMeasurement's CancelNotPassed set to false, so execution continues after failing.");
            TestExecutor.Only.MeasurementPresent.Result = EventCodes.FAIL;
            return Logger.FormatMessage(Label: "Actual", Message: Double.NaN.ToString());
        }

        internal static String DM06() {
            Debug.Assert(!TestExecutor.Only.ConfigTest.IsOperation);
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "DG-02"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Debug Measurement DM06.",
                IDPrior: "DM05",
                IDNext: "DM07",
                ClassName: MeasurementNumeric.ClassName,
                CancelNotPassed: true,
                Arguments: "High=∞|Low=-∞|SI_Units=volts|SI_Units_Modifier=DC"));
            // NOTE:  CancelNotPassed: true will cause DM06 TestMeasurement if failing.
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            // TODO:  Add measurement code here, return String result.
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Not honoring Cancellation request, thus simulating a reactive cancellation, execution terminates after TestMeasurement completes.");
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Reactive Cancellation occurring.");
            }
            return Double.PositiveInfinity.ToString();
        }

        internal static String DM07() {
            Debug.Assert(!TestExecutor.Only.ConfigTest.IsOperation);
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "DG-02"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Debug Measurement DM07.",
                IDPrior: "DM06",
                IDNext: "DM08",
                ClassName: MeasurementNumeric.ClassName,
                CancelNotPassed: false,
                Arguments: "High=∞|Low=-∞|SI_Units=volts|SI_Units_Modifier=DC"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            // TODO:  Add measurement code here, return String result.
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Not honoring Cancellation request, thus simulating a reactive cancellation, execution terminates after TestMeasurement completes.");
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Reactive Cancellation occurring.");
            }
            TestExecutor.Only.LogMessage(Label: "Note", Message: "Demonstrating both TestGroup & TestMeasurement's CancelNotPassed set to false, so execution continues after failing.");
            return Double.NaN.ToString();
        }

        internal static String DM08() {
            Debug.Assert(!TestExecutor.Only.ConfigTest.IsOperation);
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "DG-02"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Debug Measurement DM08.",
                IDPrior: "DM07",
                IDNext: TestExecutive.NONE,
                ClassName: MeasurementNumeric.ClassName,
                CancelNotPassed: false,
                Arguments: "High=∞|Low=-∞|SI_Units=volts|SI_Units_Modifier=DC"));
            //Debug.Assert(SVI.Are(
            //    P2V5: STATE.ON,
            //    P3V3: STATE.ON,
            //    P5V0: STATE.ON,
            //    VIN: STIMULUS.off,
            //    EL: LOAD_CURRENT.off,
            //    WG: FREQUENCY.off));
            // TODO:  Add measurement code here, return String result.
            Thread.Sleep(millisecondsTimeout: 1500); // NOTE:  Simulate test execution delay.
            if (TestExecutor.Only.CancelTokenSource.IsCancellationRequested) {
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Not honoring Cancellation request, thus simulating a reactive cancellation, execution terminates after TestMeasurement completes.");
                TestExecutor.Only.LogMessage(Label: "Note", Message: "Reactive Cancellation occurring.");
            }
            return Double.NegativeInfinity.ToString();
        }
        #endregion App.config GroupID "DG-01"
    }
}
