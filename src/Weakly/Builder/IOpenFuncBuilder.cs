using System;
using System.Reflection;

namespace Weakly
{
    internal interface IOpenFuncBuilder
    {
        Func<object, TResult> BuildFunc<TResult>(MethodInfo method);
        Func<object, T, TResult> BuildFunc<T, TResult>(MethodInfo method);
        Func<object, T1, T2, TResult> BuildFunc<T1, T2, TResult>(MethodInfo method);
        Func<object, T1, T2, T3, TResult> BuildFunc<T1, T2, T3, TResult>(MethodInfo method);
        Func<object, T1, T2, T3, T4, TResult> BuildFunc<T1, T2, T3, T4, TResult>(MethodInfo method);
        Func<object, T1, T2, T3, T4, T5, TResult> BuildFunc<T1, T2, T3, T4, T5, TResult>(MethodInfo method);
    }
}
