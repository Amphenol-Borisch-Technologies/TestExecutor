using System;
using System.Diagnostics;
using ABT.TestSpace.TestExec.AppConfig;
using ABT.TestSpace.TestExec.SCPI_VISA_Instruments;
using ABT.TestSpace.TestExec;
using ABT.TestSpace.UUT_Number.Instruments;

namespace ABT.TestSpace.UUT_Number.TestOperations {
    internal static partial class TestMeasurements {
        // NOTE: Invocable test methods in class TestMeasurements, defined as TestMeasurement IDs in App.config, require signatures like "internal static String MethodName()".
        #region App.config GroupID "TG-03"
        internal static String TM0900() {
            Debug.Assert(TestExecutor.Only.IsOperation(
                OperationID: "TO-02",
                Description: "Test Operation TO-02.",
                Revision: "1.0",
                GroupsIDs: "TG-03"));
            Debug.Assert(TestExecutor.Only.IsGroup(
                GroupID: "TG-03",
                Description: "Test Group TG-03, Functionality.",
                MeasurementIDs: "TM0900|TM1000|TM1100|TM1200",
                Selectable: false,
                CancelNotPassed: true));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM0900.",
                IDPrior: TestExecutive.NONE,
                IDNext: "TM1000",
                ClassName: MeasurementProcess.ClassName,
                CancelNotPassed: true,
                Arguments: "High=∞|Low=-∞|SI_Units=volts|SI_Units_Modifier=DC"));
            Debug.Assert(TestExecutor.Only.Initialized());
            SVI.Set(
                P2V5: STATE.ON,
                P3V3: STATE.ON,
                P5V0: STATE.ON,
                VIN: STIMULUS.off,
                EL: LOAD_CURRENT.off,
                WG: FREQUENCY.off);
            // TODO:  Add measurement code here, return String result.
            return String.Empty;
        }

        internal static String TM1000() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-02"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-03"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM1000.",
                IDPrior: "TM0900",
                IDNext: "TM1100",
                ClassName: MeasurementTextual.ClassName,
                CancelNotPassed: true,
                Arguments: "High=∞|Low=-∞|SI_Units=volts|SI_Units_Modifier=DC"));
            Debug.Assert(SVI.Are(
                P2V5: STATE.ON,
                P3V3: STATE.ON,
                P5V0: STATE.ON,
                VIN: STIMULUS.off,
                EL: LOAD_CURRENT.off,
                WG: FREQUENCY.off));
            // TODO:  Add measurement code here, return String result.
            return String.Empty;
        }

        internal static String TM1100() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-02"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-03"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM1100.",
                IDPrior: "TM1000",
                IDNext: "TM1200",
                ClassName: MeasurementTextual.ClassName,
                CancelNotPassed: true,
                Arguments: "High=∞|Low=-∞|SI_Units=volts|SI_Units_Modifier=DC"));
            Debug.Assert(SVI.Are(
                P2V5: STATE.ON,
                P3V3: STATE.ON,
                P5V0: STATE.ON,
                VIN: STIMULUS.off,
                EL: LOAD_CURRENT.off,
                WG: FREQUENCY.off));
            // TODO:  Add measurement code here, return String result.
            return String.Empty;
        }

        internal static String TM1200() {
            Debug.Assert(TestExecutor.Only.IsOperation("TO-02"));
            Debug.Assert(TestExecutor.Only.IsGroup(GroupID: "TG-03"));
            Debug.Assert(TestExecutor.Only.IsMeasurement(
                Description: "Test Measurement TM1200.",
                IDPrior: "TM1100",
                IDNext: TestExecutive.NONE,
                ClassName: MeasurementTextual.ClassName,
                CancelNotPassed: true,
                Arguments: "High=∞|Low=-∞|SI_Units=volts|SI_Units_Modifier=DC"));
            Debug.Assert(SVI.Are(
                P2V5: STATE.ON,
                P3V3: STATE.ON,
                P5V0: STATE.ON,
                VIN: STIMULUS.off,
                EL: LOAD_CURRENT.off,
                WG: FREQUENCY.off));
            // TODO:  Add measurement code here, return String result.
            return String.Empty;
        }
        #endregion App.config GroupID "TG-03"
    }
}

