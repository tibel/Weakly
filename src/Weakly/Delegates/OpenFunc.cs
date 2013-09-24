﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Weakly
{
    public static class OpenFunc
    {
        public static Func<object, TResult> From<TResult>(MethodInfo method)
        {
            var func = OpenDelegate.Cache.GetValueOrNull<Func<object, TResult>>(method.MethodHandle);
            if (func != null) return func;
            func = CompileFunc<TResult>(method);
            OpenDelegate.Cache.AddOrReplace(method.MethodHandle, func);
            return func;
        }

        private static Func<object, TResult> CompileFunc<TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method);
            return Expression.Lambda<Func<object, TResult>>(body, instance).Compile();
        }

        public static Func<object, T, TResult> From<T, TResult>(MethodInfo method)
        {
            var func = OpenDelegate.Cache.GetValueOrNull<Func<object, T, TResult>>(method.MethodHandle);
            if (func != null) return func;
            func = CompileFunc<T, TResult>(method);
            OpenDelegate.Cache.AddOrReplace(method.MethodHandle, func);
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

        public static Func<object, T1, T2, TResult> From<T1, T2, TResult>(MethodInfo method)
        {
            var func = OpenDelegate.Cache.GetValueOrNull<Func<object, T1, T2, TResult>>(method.MethodHandle);
            if (func != null) return func;
            func = CompileFunc<T1, T2, TResult>(method);
            OpenDelegate.Cache.AddOrReplace(method.MethodHandle, func);
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

        public static Func<object, T1, T2, T3, TResult> From<T1, T2, T3, TResult>(MethodInfo method)
        {
            var func = OpenDelegate.Cache.GetValueOrNull<Func<object, T1, T2, T3, TResult>>(method.MethodHandle);
            if (func != null) return func;
            func = CompileFunc<T1, T2, T3, TResult>(method);
            OpenDelegate.Cache.AddOrReplace(method.MethodHandle, func);
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
    }
}
