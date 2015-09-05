using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace Weakly.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class EmptyCaptureAnalyzer : DiagnosticAnalyzer
    {
        private const string Category = "Weakly";
        private const string HelpLinkUri = "https://github.com/tibel/Weakly";

        private const string AttributeUsageId = "WK1001";
        private const string AttributeUsageTitle = "Wrong usage of EmptyCaptureAttribute";
        private const string AttributeUsageMessageFormat = "Parameter '{0}' is not a delegate type";
        private const string AttributeUsageDescription = "EmptyCaptureAttribute can be applied to delegate parameters only.";

        private const string MethodCallId = "WK2001";
        private const string MethodCallTitle = "Delegate parameter caputures context";
        private const string MethodCallMessageFormat = "Method parameter '{0}' captures context";
        private const string MethodCallDescription = "Delegate parameters annotated with EmptyCaptureAttribute should not capture context (e.g. a closure or instance method).";

        private static DiagnosticDescriptor AttributeUsageRule = new DiagnosticDescriptor(AttributeUsageId, AttributeUsageTitle, AttributeUsageMessageFormat, Category,
            DiagnosticSeverity.Error, isEnabledByDefault: true, description: AttributeUsageDescription, helpLinkUri: HelpLinkUri);

        private static DiagnosticDescriptor MethodCallRule = new DiagnosticDescriptor(MethodCallId, MethodCallTitle, MethodCallMessageFormat, Category,
            DiagnosticSeverity.Warning, isEnabledByDefault: true, description: MethodCallDescription, helpLinkUri: HelpLinkUri);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(AttributeUsageRule, MethodCallRule); }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeParameter, SyntaxKind.Parameter);
            context.RegisterSyntaxNodeAction(AnalyzeArgument, SyntaxKind.Argument);
        }

        private static void AnalyzeParameter(SyntaxNodeAnalysisContext context)
        {
            var parameter = context.Node as ParameterSyntax;

            var parameterType = context.SemanticModel.GetTypeInfo(parameter.Type).Type;
            var isDelegate = parameterType.TypeKind == TypeKind.Delegate;

            var hasAttribute = parameter.AttributeLists
                .SelectMany(al => al.Attributes)
                .Any(a => context.SemanticModel.GetTypeInfo(a).Type.ToDisplayString() == "Weakly.EmptyContextAttribute");

            if (!isDelegate && hasAttribute)
            {
                var location = parameter.GetLocation();
                var parameterName = parameter.Identifier.ValueText;

                var diagnostic = Diagnostic.Create(AttributeUsageRule, location, parameterName);
                context.ReportDiagnostic(diagnostic);
            }
        }

        private void AnalyzeArgument(SyntaxNodeAnalysisContext context)
        {
            var argument = context.Node as ArgumentSyntax;
            //TODO: implement
        }
    }
}
