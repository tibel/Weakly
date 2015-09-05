using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper;

namespace Weakly.Analyzers.Test
{
    [TestClass]
    public class AttributeUsageTests : DiagnosticVerifier
    {
        [TestMethod]
        public void AttributeUsage_EmptyFile()
        {
            var test = @"";

            VerifyCSharpDiagnostic(test);
        }

        [TestMethod]
        public void AttributeUsage_Right()
        {
            var test = @"
    using System;
    using Weakly;

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            public static void Method1([EmptyCapture] Action action)
            { }

            public void Method2([EmptyCapture] Action action)
            { }
        }
    }";

            VerifyCSharpDiagnostic(test);
        }

        [TestMethod]
        public void AttributeUsage_Wrong()
        {
            var test = @"
    using System;
    using Weakly;

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            public static void Method1([EmptyCapture] int number)
            { }

            public void Method2([EmptyCapture] string name)
            { }
        }
    }";

            var method1 = new DiagnosticResult
            {
                Id = "WK1001",
                Message = string.Format("Parameter '{0}' is not a delegate type", "number"),
                Severity = DiagnosticSeverity.Error,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 9, 40) }
            };
            var method2 = new DiagnosticResult
            {
                Id = "WK1001",
                Message = string.Format("Parameter '{0}' is not a delegate type", "name"),
                Severity = DiagnosticSeverity.Error,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 12, 33) }
            };

            VerifyCSharpDiagnostic(test, method1, method2);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new EmptyCaptureAnalyzer();
        }
    }
}
