using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper to create open delegate functions.
    /// </summary>
    public static class OpenFunc
    {
        private static readonly GenericMethodCache<Delegate> Cache = new GenericMethodCache<Delegate>();

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Func<object, TResult> From<TResult>(MethodInfo method)
        {
            var func = Cache.GetValueOrNull<Func<object, TResult>>(method);
            if (func != null) return func;
            func = CompileFunc<TResult>(method);
            Cache.AddOrReplace(method, func);
            return func;
        }

        private static Func<object, TResult> CompileFunc<TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method);
            return Expression.Lambda<Func<object, TResult>>(body, instance).Compile();
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
            var func = Cache.GetValueOrNull<Func<object, T, TResult>>(method);
            if (func != null) return func;
            func = CompileFunc<T, TResult>(method);
            Cache.AddOrReplace(method, func);
            return func;
        }

        private static Func<object, T, TResult> CompileFunc<T, TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var obj = Expression.Parameter(typeof(T), "obj");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, obj);
            return Expression.Lambda<Func<object, T, TResult>>(body, instance, obj).Compile();
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
            var func = Cache.GetValueOrNull<Func<object, T1, T2, TResult>>(method);
            if (func != null) return func;
            func = CompileFunc<T1, T2, TResult>(method);
            Cache.AddOrReplace(method, func);
            return func;
        }

        private static Func<object, T1, T2, TResult> CompileFunc<T1, T2, TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var arg1 = Expression.Parameter(typeof(T1), "arg1");
            var arg2 = Expression.Parameter(typeof(T2), "arg2");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, arg1, arg2);
            return Expression.Lambda<Func<object, T1, T2, TResult>>(body, instance, arg1, arg2).Compile();
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
            var func = Cache.GetValueOrNull<Func<object, T1, T2, T3, TResult>>(method);
            if (func != null) return func;
            func = CompileFunc<T1, T2, T3, TResult>(method);
            Cache.AddOrReplace(method, func);
            return func;
        }

        private static Func<object, T1, T2, T3, TResult> CompileFunc<T1, T2, T3, TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var arg1 = Expression.Parameter(typeof(T1), "arg1");
            var arg2 = Expression.Parameter(typeof(T2), "arg2");
            var arg3 = Expression.Parameter(typeof(T3), "arg3");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, arg1, arg2, arg3);
            return Expression.Lambda<Func<object, T1, T2, T3, TResult>>(body, instance, arg1, arg2, arg3).Compile();
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
            var func = Cache.GetValueOrNull<Func<object, T1, T2, T3, T4, TResult>>(method);
            if (func != null) return func;
            func = CompileFunc<T1, T2, T3, T4, TResult>(method);
            Cache.AddOrReplace(method, func);
            return func;
        }

        private static Func<object, T1, T2, T3, T4, TResult> CompileFunc<T1, T2, T3, T4, TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var arg1 = Expression.Parameter(typeof(T1), "arg1");
            var arg2 = Expression.Parameter(typeof(T2), "arg2");
            var arg3 = Expression.Parameter(typeof(T3), "arg3");
            var arg4 = Expression.Parameter(typeof(T4), "arg4");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, arg1, arg2, arg3, arg4);
            return Expression.Lambda<Func<object, T1, T2, T3, T4, TResult>>(body, instance, arg1, arg2, arg3, arg4).Compile();
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
            var func = Cache.GetValueOrNull<Func<object, T1, T2, T3, T4, T5, TResult>>(method);
            if (func != null) return func;
            func = CompileFunc<T1, T2, T3, T4, T5, TResult>(method);
            Cache.AddOrReplace(method, func);
            return func;
        }

        private static Func<object, T1, T2, T3, T4, T5, TResult> CompileFunc<T1, T2, T3, T4, T5, TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var arg1 = Expression.Parameter(typeof(T1), "arg1");
            var arg2 = Expression.Parameter(typeof(T2), "arg2");
            var arg3 = Expression.Parameter(typeof(T3), "arg3");
            var arg4 = Expression.Parameter(typeof(T4), "arg4");
            var arg5 = Expression.Parameter(typeof(T5), "arg5");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, arg1, arg2, arg3, arg4, arg5);
            return Expression.Lambda<Func<object, T1, T2, T3, T4, T5, TResult>>(body, instance, arg1, arg2, arg3, arg4, arg5).Compile();
        }
    }
}
