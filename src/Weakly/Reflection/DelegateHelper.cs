using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Weakly
{
    /// <summary>
    /// Some useful helpers for <see cref="System.Reflection"/>.
    /// </summary>
    public static class DelegateHelper
    {
        /// <summary>
        /// Determines whether the specified method is a lambda.
        /// </summary>
        /// <param name="methodInfo">The method to examine.</param>
        /// <returns>True, if the method is a lambda; otherwise false.</returns>
        public static bool IsLambda(this MethodInfo methodInfo)
        {
            return methodInfo.IsStatic && methodInfo.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
        }

        /// <summary>
        /// Determines whether the specified method is closure.
        /// </summary>
        /// <param name="methodInfo">The method to examine.</param>
        /// <returns>True, if the method is a closure; otherwise false.</returns>
        public static bool IsClosure(this MethodInfo methodInfo)
        {
            return !methodInfo.IsStatic &&
                   methodInfo.DeclaringType.GetTypeInfo().GetCustomAttribute<CompilerGeneratedAttribute>() != null;
        }

        /// <summary>
        /// Determines whether the specified method is an async method.
        /// </summary>
        /// <param name="methodInfo">The method to examine.</param>
        /// <returns>True, if the method is an async method; otherwise false.</returns>
        public static bool IsAsync(this MethodInfo methodInfo)
        {
            return methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>() != null;
        }

        /// <summary>
        /// Determines whether the specified handler is closure.
        /// </summary>
        /// <param name="handler">The delegate to examine.</param>
        /// <returns>True, if the <paramref name="handler"/> is a closure; otherwise false.</returns>
        public static bool IsClosure(this Delegate handler)
        {
            return handler.Target != null && handler.GetMethodInfo().IsClosure();
        }
    }
}
