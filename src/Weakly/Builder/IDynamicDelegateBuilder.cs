using System;
using System.Reflection;

namespace Weakly
{
    internal interface IDynamicDelegateBuilder
    {
        Func<object, object[], object> BuildDynamic(MethodInfo method);
    }
}
