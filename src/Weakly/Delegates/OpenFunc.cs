using System;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper to create open delegate functions.
    /// </summary>
    public static class OpenFunc
    {
        private static readonly IOpenFuncBuilder Builder = new CachingOpenFuncBuilderDecorator(new ExpressionOpenFuncBuilder());

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Func<object, TResult> From<TResult>(MethodInfo method)
        {
            return Builder.BuildFunc<TResult>(method);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Func<object, T, TResult> From<T, TResult>(MethodInfo method)
        {
            return Builder.BuildFunc<T, TResult>(method);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Func<object, T1, T2, TResult> From<T1, T2, TResult>(MethodInfo method)
        {
            return Builder.BuildFunc<T1, T2, TResult>(method);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate</returns>
        public static Func<object, T1, T2, T3, TResult> From<T1, T2, T3, TResult>(MethodInfo method)
        {
            return Builder.BuildFunc<T1, T2, T3, TResult>(method);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate</returns>
        public static Func<object, T1, T2, T3, T4, TResult> From<T1, T2, T3, T4, TResult>(MethodInfo method)
        {
            return Builder.BuildFunc<T1, T2, T3, T4, TResult>(method);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate</returns>
        public static Func<object, T1, T2, T3, T4, T5, TResult> From<T1, T2, T3, T4, T5, TResult>(MethodInfo method)
        {
            return Builder.BuildFunc<T1, T2, T3, T4, T5, TResult>(method);
        }
    }
}
