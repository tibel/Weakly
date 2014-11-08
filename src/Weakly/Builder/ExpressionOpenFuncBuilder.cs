using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Weakly
{
    internal class ExpressionOpenFuncBuilder : IOpenFuncBuilder
    {
        public Func<object, TResult> BuildFunc<TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method);
            return Expression.Lambda<Func<object, TResult>>(body, instance).Compile();
        }

        public Func<object, T, TResult> BuildFunc<T, TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var obj = Expression.Parameter(typeof(T), "obj");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, obj);
            return Expression.Lambda<Func<object, T, TResult>>(body, instance, obj).Compile();
        }

        public Func<object, T1, T2, TResult> BuildFunc<T1, T2, TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var arg1 = Expression.Parameter(typeof(T1), "arg1");
            var arg2 = Expression.Parameter(typeof(T2), "arg2");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, arg1, arg2);
            return Expression.Lambda<Func<object, T1, T2, TResult>>(body, instance, arg1, arg2).Compile();
        }

        public Func<object, T1, T2, T3, TResult> BuildFunc<T1, T2, T3, TResult>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var arg1 = Expression.Parameter(typeof(T1), "arg1");
            var arg2 = Expression.Parameter(typeof(T2), "arg2");
            var arg3 = Expression.Parameter(typeof(T3), "arg3");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, arg1, arg2, arg3);
            return Expression.Lambda<Func<object, T1, T2, T3, TResult>>(body, instance, arg1, arg2, arg3).Compile();
        }

        public Func<object, T1, T2, T3, T4, TResult> BuildFunc<T1, T2, T3, T4, TResult>(MethodInfo method)
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

        public Func<object, T1, T2, T3, T4, T5, TResult> BuildFunc<T1, T2, T3, T4, T5, TResult>(MethodInfo method)
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
