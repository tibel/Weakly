using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
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

            //context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ParenthesizedLambdaExpression, SyntaxKind.SimpleLambdaExpression, SyntaxKind.AnonymousMethodExpression);
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

                //argumentList.Arguments[0].DetermineParameter(context.SemanticModel)

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

        private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var node = context.Node;
            var semanticModel = context.SemanticModel;
            var cancellationToken = context.CancellationToken;
            Action<Diagnostic> reportDiagnostic = context.ReportDiagnostic;

            var anonExpr = node as AnonymousMethodExpressionSyntax;
            if (anonExpr?.Block?.ChildNodes() != null && anonExpr.Block.ChildNodes().Any())
            {
                ClosureCaptureDataFlowAnalysis(semanticModel.AnalyzeDataFlow(anonExpr.Block.ChildNodes().First(), anonExpr.Block.ChildNodes().Last()), reportDiagnostic, anonExpr.DelegateKeyword.GetLocation());
                return;
            }

            var lambdaExpr = node as SimpleLambdaExpressionSyntax;
            if (lambdaExpr != null)
            {
                ClosureCaptureDataFlowAnalysis(semanticModel.AnalyzeDataFlow(lambdaExpr), reportDiagnostic, lambdaExpr.ArrowToken.GetLocation());
                return;
            }

            var parenLambdaExpr = node as ParenthesizedLambdaExpressionSyntax;
            if (parenLambdaExpr != null)
            {
                ClosureCaptureDataFlowAnalysis(semanticModel.AnalyzeDataFlow(parenLambdaExpr), reportDiagnostic, parenLambdaExpr.ArrowToken.GetLocation());
                return;
            }
        }

        private static void ClosureCaptureDataFlowAnalysis(DataFlowAnalysis flow, Action<Diagnostic> reportDiagnostic, Location location)
        {
            if (flow != null && flow.DataFlowsIn != null)
            {
                var captures = new List<string>();
                foreach (var dfaIn in flow.DataFlowsIn)
                {
                    if (dfaIn.Name != null && dfaIn.Locations != null)
                    {
                        captures.Add(dfaIn.Name);
                        foreach (var l in dfaIn.Locations)
                        {
                            reportDiagnostic(Diagnostic.Create(ClosureCaptureRule, l, EmptyMessageArgs));
                        }
                    }
                }

                if (captures.Count > 0)
                {
                    reportDiagnostic(Diagnostic.Create(ClosureDriverRule, location, new object[] { string.Join(",", captures) }));
                }
            }
        }
    }
}
