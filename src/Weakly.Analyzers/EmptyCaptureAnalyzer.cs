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
            context.RegisterSymbolAction(AnalyzeMethod, SymbolKind.Method);
            context.RegisterSyntaxNodeAction(AnalyzeInvocation, SyntaxKind.InvocationExpression);
        }

        private static void AnalyzeMethod(SymbolAnalysisContext context)
        {
            var method = context.Symbol as IMethodSymbol;
            var contractSymbol = context.Compilation.GetTypeByMetadataName("Weakly.EmptyCaptureAttribute");

            foreach (var parmeter in method.Parameters)
            {
                var contractAttribute = parmeter.GetAttributes().FirstOrDefault(a => a.AttributeClass.Equals(contractSymbol));

                if (contractAttribute != null && parmeter.Type.TypeKind != TypeKind.Delegate)
                {
                    var location = Location.Create(contractAttribute.ApplicationSyntaxReference.SyntaxTree, contractAttribute.ApplicationSyntaxReference.Span);

                    var diagnostic = Diagnostic.Create(AttributeUsageRule, location, parmeter.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
        
        private static void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = context.Node as InvocationExpressionSyntax;

            var method = context.SemanticModel.GetSymbolInfo(invocationExpression).Symbol as IMethodSymbol;

            var contractSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName("Weakly.EmptyCaptureAttribute");

            var argumentList = invocationExpression.ArgumentList;
            if ((argumentList?.Arguments.Count ?? 0) > 0)
            {
                var x = argumentList.Arguments[0].Expression;
                //argumentList.Arguments[0]

                //context.SemanticModel.AnalyzeDataFlow()
            }


            foreach (var parmeter in method.Parameters)
            {
                var contractAttribute = parmeter.GetAttributes().FirstOrDefault(a => a.AttributeClass.Equals(contractSymbol));

                if (contractAttribute != null && parmeter.Type.TypeKind == TypeKind.Delegate)
                {
                    //TODO: analyze argument
                }
            }
        }
    }
}
