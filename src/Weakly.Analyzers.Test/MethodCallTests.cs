using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;
using Weakly.Analyzers;

namespace Weakly.Analyzers.Test
{
    [TestClass]
    public class MethodCallTests : DiagnosticVerifier
    {
        [TestMethod]
        public void MethodCall_Lambda()
        {
            var test = @"
    using System;
    using Weakly;

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            private static void Method1([EmptyCapture] Action<int> action)
            { }

            public static void Main()
            {
                 Method1(i => { });
            }
        }
    }";

            VerifyCSharpDiagnostic(test);
        }

        [TestMethod]
        public void MethodCall_StaticMethod()
        {
            var test = @"
    using System;
    using Weakly;

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            private static void Method1([EmptyCapture] Action<int> action)
            { }

            private static void Method2(int number)
            { }

            public static void Main()
            {
                 Method1(Method2);
            }
        }
    }";

            VerifyCSharpDiagnostic(test);
        }


        [TestMethod]
        public void MethodCall_Closure()
        {
            var test = @"
    using System;
    using Weakly;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            private static void Method1([EmptyCapture] Action<int> action)
            { }

            public static void Main()
            {
                 int auto;
                 Method1(i => { auto = 1; });
            }
        }
    }";

            var Main = new DiagnosticResult
            {
                Id = "WK2001",
                Message = string.Format("Method parameter '{0}' captures context", "action"),
                Severity = DiagnosticSeverity.Warning,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 18, 26) }
            };

            VerifyCSharpDiagnostic(test, Main);
        }

        [TestMethod]
        public void MethodCall_InstanceMethod()
        {
            var test = @"
    using System;
    using Weakly;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            private static void Method1([EmptyCapture] Action<int> action)
            { }

            private void Method2(int number)
            { }

            public static void Main()
            {
                 var instance = new TypeName();
                 Method1(instance.Method2);
            }
        }
    }";

            var Main = new DiagnosticResult
            {
                Id = "WK2001",
                Message = string.Format("Method parameter '{0}' captures context", "action"),
                Severity = DiagnosticSeverity.Warning,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 18, 26) }
            };

            VerifyCSharpDiagnostic(test, Main);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new EmptyCaptureAnalyzer();
        }
    }
}
