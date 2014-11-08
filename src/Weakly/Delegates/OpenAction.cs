using System;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper to create open delegate actions.
    /// </summary>
    public static class OpenAction
    {
        private static readonly IOpenActionBuilder Builder = new CachingOpenActionBuilderDecorator(new ExpressionOpenActionBuilder());

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Action<object> From(MethodInfo method)
        {
            return Builder.BuildAction(method);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Action<object, T> From<T>(MethodInfo method)
        {
            return Builder.BuildAction<T>(method);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Action<object, T1, T2> From<T1, T2>(MethodInfo method)
        {
            return Builder.BuildAction<T1, T2>(method);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Action<object, T1, T2, T3> From<T1, T2, T3>(MethodInfo method)
        {
            return Builder.BuildAction<T1, T2, T3>(method);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Action<object, T1, T2, T3, T4> From<T1, T2, T3, T4>(MethodInfo method)
        {
            return Builder.BuildAction<T1, T2, T3, T4>(method);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Action<object, T1, T2, T3, T4, T5> From<T1, T2, T3, T4, T5>(MethodInfo method)
        {
            return Builder.BuildAction<T1, T2, T3, T4, T5>(method);
        }
    }
}
