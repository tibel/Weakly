using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Weakly.Builders
{
    /// <summary>
    /// <see cref="Expression"/> based <see cref="IDynamicDelegateBuilder"/>.
    /// </summary>
    public class ExpressionDynamicDelegateBuilder : IDynamicDelegateBuilder
    {
        /// <summary>
        /// Create a dynamic delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The dynamic delegate.
        /// </returns>
        public Func<object, object[], object> BuildDynamic(MethodInfo method)
        {
            var parameterInfos = method.GetParameters();

            var instance = Expression.Parameter(typeof(object), "instance");
            var parameters = Expression.Parameter(typeof(object[]), "parameters");
            var instructions = new List<Expression>();

            var checkParametersLength = CheckParametersLength(parameters, parameterInfos.Length);
            instructions.Add(checkParametersLength);

            var typedInstance = ConvertInstance(instance, method);
            var parameterArray = ConvertParameters(parameters, parameterInfos);
            var methodCall = Expression.Call(typedInstance, method, parameterArray);

            if (method.ReturnType != typeof(void))
            {
                var convertedResult = Expression.Convert(methodCall, typeof(object));
                instructions.Add(convertedResult);
            }
            else
            {
                instructions.Add(methodCall);
                instructions.Add(Expression.Constant(null, typeof(object)));
            }

            var body = Expression.Block(instructions);
            return Expression.Lambda<Func<object, object[], object>>(body, instance, parameters).Compile();
        }

        private static Expression ConvertInstance(Expression instance, MethodBase method)
        {
            Expression typedInstance = null;
            if (!method.IsStatic)
            {
                typedInstance = Expression.Convert(instance, method.DeclaringType);
            }
            return typedInstance;
        }

        private static Expression[] ConvertParameters(ParameterExpression parameters, ParameterInfo[] parameterInfos)
        {
            var parameterArray = new Expression[parameterInfos.Length];
            for (var i = 0; i < parameterInfos.Length; i++)
            {
                var p = Expression.ArrayIndex(parameters, Expression.Constant(i, typeof(int)));
                parameterArray[i] = Expression.Convert(p, parameterInfos[i].ParameterType);
            }
            return parameterArray;
        }

        private static ConditionalExpression CheckParametersLength(Expression parameters, int length)
        {
            return Expression.IfThen(
                Expression.NotEqual(
                    Expression.ArrayLength(parameters),
                    Expression.Constant(length, typeof(int))),
                Expression.Throw(Expression.Constant(new TargetParameterCountException())));
        }
    }
}
