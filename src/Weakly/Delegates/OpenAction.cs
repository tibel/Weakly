using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Weakly
{
    public static class OpenAction
    {
        public static Action<object> From(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method);
            return Expression.Lambda<Action<object>>(body, instance).Compile();
        }

        public static Action<object, T> From<T>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var obj = Expression.Parameter(typeof(T), "obj");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, obj);
            return Expression.Lambda<Action<object, T>>(body, instance, obj).Compile();
        }

        public static Action<object, T1, T2> From<T1, T2>(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var arg1 = Expression.Parameter(typeof(T1), "arg1");
            var arg2 = Expression.Parameter(typeof(T2), "arg2");

            var typedInstance = Expression.Convert(instance, method.DeclaringType);
            var body = Expression.Call(typedInstance, method, arg1, arg2);
            return Expression.Lambda<Action<object, T1, T2>>(body, instance, arg1, arg2).Compile();
        }

        public static Action<object, T1, T2, T3> From<T1, T2, T3>(MethodInfo method)
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
