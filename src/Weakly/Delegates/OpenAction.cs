using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper to create open delegate actions.
    /// </summary>
    public static class OpenAction
    {
        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Action<object> From(MethodInfo method)
        {
            var action = OpenDelegate.Cache.GetValueOrNull<Action<object>>(method.MethodHandle);
            if (action != null) return action;
            action = CompileAction(method);
            OpenDelegate.Cache.AddOrReplace(method.MethodHandle, action);
            return action;
        }

        private static Action<object> CompileAction(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof (object), "instance");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method);
            return Expression.Lambda<Action<object>>(body, instance).Compile();
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The open delegate.</returns>
        public static Action<object, T> From<T>(MethodInfo method)
        {
            var action = OpenDelegate.Cache.GetValueOrNull<Action<object, T>>(method.MethodHandle);
            if (action != null) return action;
            action = CompileAction<T>(method);
            OpenDelegate.Cache.AddOrReplace(method.MethodHandle, action);
            return action;
        }

        private static Action<object, T> CompileAction<T>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof (object), "instance");
            var obj = Expression.Parameter(typeof (T), "obj");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, obj);
            return Expression.Lambda<Action<object, T>>(body, instance, obj).Compile();
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
            var action = OpenDelegate.Cache.GetValueOrNull<Action<object, T1, T2>>(method.MethodHandle);
            if (action != null) return action;
            action = CompileAction<T1, T2>(method);
            OpenDelegate.Cache.AddOrReplace(method.MethodHandle, action);
            return action;
        }

        private static Action<object, T1, T2> CompileAction<T1, T2>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof (object), "instance");
            var arg1 = Expression.Parameter(typeof (T1), "arg1");
            var arg2 = Expression.Parameter(typeof (T2), "arg2");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, arg1, arg2);
            return Expression.Lambda<Action<object, T1, T2>>(body, instance, arg1, arg2).Compile();
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
            var action = OpenDelegate.Cache.GetValueOrNull<Action<object, T1, T2, T3>>(method.MethodHandle);
            if (action != null) return action;
            action = CompileAction<T1, T2, T3>(method);
            OpenDelegate.Cache.AddOrReplace(method.MethodHandle, action);
            return action;
        }

        private static Action<object, T1, T2, T3> CompileAction<T1, T2, T3>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var arg1 = Expression.Parameter(typeof(T1), "arg1");
            var arg2 = Expression.Parameter(typeof(T2), "arg2");
            var arg3 = Expression.Parameter(typeof(T3), "arg3");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, arg1, arg2, arg3);
            return Expression.Lambda<Action<object, T1, T2, T3>>(body, instance, arg1, arg2, arg3).Compile();
        }
    }
}
