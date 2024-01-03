using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

// NOTE:  Recommend using Microsoft's Visual Studio Code to develop/debug TestExecutor based closed source/proprietary projects:
//        - Visual Studio Code is a co$t free, open-source Integrated Development Environment entirely suitable for textual C# development, like TestExecutor.Only.
//          - That is, it's excellent for non-GUI (WinForms/WPF/UWP/WinUI 3) C# development.
//          - VS Code is free for both private & commercial use:
//            - https://code.visualstudio.com/docs/supporting/FAQ
//            - https://code.visualstudio.com/license
// NOTE:  Recommend using Microsoft's Visual Studio Community Edition to develop/debug open sourced TestExecutive:
//        - https://github.com/Amphenol-Borisch-Technologies/TestExecutive/blob/master/LICENSE.txt
//        - "An unlimited number of users within an organization can use Visual Studio Community for the following scenarios:
//           in a classroom learning environment, for academic research, or for contributing to open source projects."
//        - TestExecutor based projects are very likely closed source/proprietary, which are disqualified from using VS Studio Community Edition.
//          - https://visualstudio.microsoft.com/vs/community/
//          - https://visualstudio.microsoft.com/license-terms/vs2022-ga-community/
// NOTE:  - VS Studio Community Edition is more preferable for GUI C# development than VS Code.
//          - If not developing GUI code (WinForms/WPF/UWP/WinUI 3), then VS Code is entirely sufficient & potentially preferable.
// TODO:  Eventually; refactor to Microsoft's C# Coding Conventions, https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions.
// TODO:  Eventually; add documentation per https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments.
// TODO:  Soon; ensure Borisch Domain Group "Test - Engineers" has read & write permissions on all TestExecutive & TestExecutor folder/files.
// TODO:  Soon; ensure Borisch Domain Groups ≠ "Test - Engineers" have only read permissions on all TestExecutive & TestExecutor folder/files.
// TODO:  Soon; find & replace all other possible absolute paths with relative paths.
// NOTE:  For public methods, will deviate by using PascalCasing for parameters.  Will use recommended camelCasing for internal & private method parameters.
//        - Prefer named arguments for public methods be Capitalized/PascalCased, not uncapitalized/camelCased.
//        - Invoking public methods with named arguments is a superb, self-documenting coding technique, improved by PascalCasing.
// NOTE:  Used .Net FrameWork 4.8 instead of .Net 7.0 because required Texas instruments' TIDP.SAA Fusion API targets
//        .Net FrameWork 2.0, incompatible with .Net 7.0, C# 11.0 & WinUI 3.
// NOTE:  Liberal usage of Debug.Asserts, which improve code readability & quality and provide better documentation than comments, since they'll fail if they become outdated.
//        - https://stackoverflow.com/questions/129120/when-should-i-use-debug-assert
//        - https://softwareengineering.stackexchange.com/questions/87250/is-possible-to-write-too-many-asserts/307012#307012
//        - https://stackoverflow.com/questions/13856525/in-c-is-a-debug-assert-test-run-in-release-mode
// NOTE:  MPLAB IPE's command line instructions reside in this project's Resources folder as file 'Readme for IPECMD.htm'.
//        - https://microchipdeveloper.com/faq:261

/// <para>
///  References:
///  - https://github.com/Amphenol-Borisch-Technologies/TestExecutive
///  - https://github.com/Amphenol-Borisch-Technologies/TestExecutor
///  </para>

/// <summary>
/// NOTE:  Test Developer is responsible for ensuring Measurements can be both safely & correctly called in sequence defined in App.config:
/// <para>
///        - That is, if Measurements execute sequentially as (M1, M2, M3, M4, M5), Test Developer is responsible for ensuring all equipment is
///          configured safely & correctly between each Measurement step.
///          - If:
///            - M1 is unpowered Shorts & Opens measurements.
///            - M2 is powered voltage measurements.
///            - M3 begins with unpowered operator cable connections/disconnections for In-System Programming.
///          - Then Test Developer must ensure necessary equipment state transitions are implemented so test operator isn't
///            plugging/unplugging a powered UUT in T03.
/// </para>
/// </summary>
/// 
/// <summary>
/// NOTE:  Two types of TestExecutor Cancellations possible, each having two sub-types resulting in 4 altogether:
/// <para>
/// A) Spontaneous Operator Initiated Cancellations:
///      1)  Operator Proactive:
///          - Microsoft's recommended CancellationTokenSource technique, permitting Operator to proactively
///            cancel currently executing Measurement.
///          - Requires TestExecutor implementation by the Test Developer, but is initiated by Operator, so categorized as such.
///          - Implementation necessary if the *currently* executing Measurement must be cancellable during execution by the Operator.
///          - https://learn.microsoft.com/en-us/dotnet/standard/threading/cancellation-in-managed-threads
///          - https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation
///          - https://learn.microsoft.com/en-us/dotnet/standard/threading/canceling-threads-cooperatively
///      2)  Operator Reactive:
///          - TestExecutive's already implemented, always available & default reactive "Cancel before next Test" technique,
///            which simply sets _cancelled Boolean to true, checked at the end of TestExecutive.MeasurementsRun()'s foreach loop.
///          - If _cancelled is true, TestExecutive.MeasurementsRun()'s foreach loop is broken, causing reactive cancellation
///            prior to the next Measurement's execution.
///          - Note: This doesn't proactively cancel the *currently* executing Measurement, which runs to completion.
/// B) PrePlanned Developer Programmed Cancellations:
///      3)  TestExecutor/Test Developer initiated Cancellations:
///          - Any TestExecutor's Measurement can initiate a Cancellation programmatically by simply throwing a CancellationException:
///          - Permits immediate Cancellation if specific condition(s) occur in a Measurement; perhaps to prevent UUT or equipment damage,
///            or simply because futher execution is pointless.
///          - Simply throw a CancellationException if the specific condition(s) occcur.
///      4)  App.config's CancelNotPassed:
///          - App.config's TestMeasurement element has a Boolean "CancelNotPassed" field:
///          - If the current TestExecutor.Only.MeasurementRun() has CancelNotPassed=true and it's resulting EvaluateResultMeasurement() doesn't return EventCodes.PASS,
///            TestExecutive.MeasurementsRun() will break/exit, stopping further testing.
///		    - Do not pass Go, do not collect $200, go directly to TestExecutive.MeasurementsPostRun().
///
/// NOTE:  The Operator Proactive & TestExecutor/Test Developer initiated Cancellations both occur while the currently executing TestExecutor.Only.MeasurementRun() conpletes, via 
///        thrown CancellationExceptions.
/// NOTE:  The Operator Reactive & App.config's CancelNotPassed Cancellations both occur after the currently executing TestExecutor.Only.MeasurementRun() completes, via checks
///        inside the TestExecutive.MeasurementsRun() loop.
/// </para>
/// </summary>
namespace ABT.TestSpace.TestExec {
    internal class TestExecutorMain {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try { Application.Run(TestExecutor.Only); } catch (Exception e) { TestExecutive.ErrorMessage(e); }
        }
    }

    internal sealed class TestExecutor : TestExecutive {
        private static readonly TestExecutor _only = new TestExecutor();
        internal static TestExecutor Only { get { return _only; } }

        static TestExecutor() { }
        /// <summary>
        /// Singleton pattern requires explicit static constructor to tell C# compiler not to mark type as beforefieldinit.
        /// https://csharpindepth.com/articles/singleton
        /// <para>
        ///  - Utilized Singleton for TestExecutor class because there should only ever be 1 instance of TestExecutor.Only.
        ///  - Also, TestExecutor being a Singleton eliminates needing to pass it's instance to all TestExecutor methods, most which don't require it, which generates annoying compiler warnings.
        ///    - Realize both mayn't be optimal practices, and may refactor TestExecutor to a non-Singleton class, and resume explicitly passing TestExecutor object into methods.
        /// </para>
        /// </summary>
        private TestExecutor() : base(Properties.Resources.Amphenol) {
            // NOTE:  Change base constructor's Icon as applicable, depending on customer.
            // https://stackoverflow.com/questions/40933304/how-to-create-an-icon-for-visual-studio-with-just-mspaint-and-visual-studio
        }

        protected override async Task<String> MeasurementRun(String measurementID) {
            Type type = Type.GetType("ABT.TestSpace.UUT_Number.TestOperations.TestMeasurements");
            // NOTE:  Will only seek invocable measurement methods in class TestMeasurements that are defined as TestMeasurement IDs in App.config & and are part of a Group.
            MethodInfo methodInfo = type.GetMethod(MeasurementIDPresent, BindingFlags.Static | BindingFlags.NonPublic);
            // NOTE:  Invocable measurement methods in class TestMeasurements, defined as TestMeasurement IDs in App.config, must have signatures identical to "internal static String MethodName()",
            // or "private static String MethodName()", though the latter are discouraged for consistency.
            Object task = await Task.Run(() => methodInfo.Invoke(null, null));
            return (String)task;
        }

        public override void Initialize() {
            if (!Simulate) {
                base.Initialize();
                // NOTE:  Add custom Initialization here.
                Debug.Assert(Only.Initialized());
            }
        }

        public override Boolean Initialized() {
            if (!Simulate) return base.Initialized(); // NOTE:  && with custom Initialization here.
            return false;
        }
    }
}
