﻿using System;
using System.Reflection;

namespace Weakly.Builders
{
    /// <summary>
    /// Reflection based <see cref="IOpenFuncBuilder"/>.
    /// </summary>
    public sealed class ReflectionOpenFuncBuilder : IOpenFuncBuilder
    {
        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Func<object, TResult> BuildFunc<TResult>(MethodInfo method)
        {
            return instance => (TResult) method.Invoke(instance, null);
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Func<object, T, TResult> BuildFunc<T, TResult>(MethodInfo method)
        {
            return (instance, arg0) => (TResult) method.Invoke(instance, new object[] {arg0});
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Func<object, T1, T2, TResult> BuildFunc<T1, T2, TResult>(MethodInfo method)
        {
            return (instance, arg0, arg1) => (TResult)method.Invoke(instance, new object[] { arg0, arg1 });
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate
        /// </returns>
        public Func<object, T1, T2, T3, TResult> BuildFunc<T1, T2, T3, TResult>(MethodInfo method)
        {
            return (instance, arg0, arg1, arg2) => (TResult) method.Invoke(instance, new object[] {arg0, arg1, arg2});
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
        /// <returns>
        /// The open delegate
        /// </returns>
        public Func<object, T1, T2, T3, T4, TResult> BuildFunc<T1, T2, T3, T4, TResult>(MethodInfo method)
        {
            return (instance, arg0, arg1, arg2, arg3) => (TResult)method.Invoke(instance, new object[] { arg0, arg1, arg2, arg3 });
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
        /// <returns>
        /// The open delegate
        /// </returns>
        public Func<object, T1, T2, T3, T4, T5, TResult> BuildFunc<T1, T2, T3, T4, T5, TResult>(MethodInfo method)
        {
            return (instance, arg0, arg1, arg2, arg3, arg4) => (TResult)method.Invoke(instance, new object[] { arg0, arg1, arg2, arg3, arg4 });
        }
    }
}
