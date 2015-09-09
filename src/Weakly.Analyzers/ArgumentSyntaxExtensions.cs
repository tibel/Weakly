using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Weakly.Analyzers
{
    internal static class ArgumentSyntaxExtensions
    {
        /// <summary>
        /// Returns the parameter to which this argument is passed. If <paramref name="allowParams"/>
        /// is true, the last parameter will be returned if it is params parameter and the index of
        /// the specified argument is greater than the number of parameters.
        /// </summary>
        public static IParameterSymbol DetermineParameter(
            this ArgumentSyntax argument,
            SemanticModel semanticModel,
            bool allowParams = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var argumentList = argument.Parent as BaseArgumentListSyntax;
            if (argumentList == null)
            {
                return null;
            }

            var invocableExpression = argumentList.Parent as ExpressionSyntax;
            if (invocableExpression == null)
            {
                return null;
            }

            var symbol = semanticModel.GetSymbolInfo(invocableExpression, cancellationToken).Symbol as IMethodSymbol;
            if (symbol == null)
            {
                return null;
            }

            var parameters = symbol.Parameters;

            // Handle named argument
            if (argument.NameColon != null && !argument.NameColon.IsMissing)
            {
                var name = argument.NameColon.Name.Identifier.ValueText;
                return parameters.FirstOrDefault(p => p.Name == name);
            }

            // Handle positional argument
            var index = argumentList.Arguments.IndexOf(argument);
            if (index < 0)
            {
                return null;
            }

            if (index < parameters.Length)
            {
                return parameters[index];
            }

            if (allowParams)
            {
                var lastParameter = parameters.LastOrDefault();
                if (lastParameter == null)
                {
                    return null;
                }

                if (lastParameter.IsParams)
                {
                    return lastParameter;
                }
            }

            return null;
        }
    }
}
