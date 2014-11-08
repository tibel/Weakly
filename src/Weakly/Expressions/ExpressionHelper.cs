using System.Linq.Expressions;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Common extensions for <see cref="Expression"/>.
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// Converts an expression into a <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="expression">The expression to convert.</param>
        /// <returns>The member info.</returns>
        public static MemberInfo GetMemberInfo(Expression expression)
        {
            expression = ((LambdaExpression)expression).Body;

            if (expression.NodeType == ExpressionType.Convert)
            {
                expression = ((UnaryExpression)expression).Operand;
            }

            var memberExpression = (MemberExpression) expression;
            return memberExpression.Member;
        }
    }
}
