using System;
using System.Reflection;

namespace Weakly
{
    internal interface IOpenActionBuilder
    {
        Action<object> BuildAction(MethodInfo method);
        Action<object, T> BuildAction<T>(MethodInfo method);
        Action<object, T1, T2> BuildAction<T1, T2>(MethodInfo method);
        Action<object, T1, T2, T3> BuildAction<T1, T2, T3>(MethodInfo method);
        Action<object, T1, T2, T3, T4> BuildAction<T1, T2, T3, T4>(MethodInfo method);
        Action<object, T1, T2, T3, T4, T5> BuildAction<T1, T2, T3, T4, T5>(MethodInfo method);
    }
}
