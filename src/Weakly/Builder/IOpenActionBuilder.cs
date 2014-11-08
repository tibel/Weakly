using System;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Creates open delegate actions.
    /// </summary>
    internal interface IOpenActionBuilder
    {
        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        Action<object> BuildAction(MethodInfo method);

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        Action<object, T> BuildAction<T>(MethodInfo method);

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        Action<object, T1, T2> BuildAction<T1, T2>(MethodInfo method);

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        Action<object, T1, T2, T3> BuildAction<T1, T2, T3>(MethodInfo method);

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        Action<object, T1, T2, T3, T4> BuildAction<T1, T2, T3, T4>(MethodInfo method);

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
        Action<object, T1, T2, T3, T4, T5> BuildAction<T1, T2, T3, T4, T5>(MethodInfo method);
    }
}
