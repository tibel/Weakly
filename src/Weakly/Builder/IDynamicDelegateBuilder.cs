using System;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Creates dynamic delegate functions.
    /// </summary>
    internal interface IDynamicDelegateBuilder
    {
        /// <summary>
        /// Create a dynamic delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The dynamic delegate.</returns>
        Func<object, object[], object> BuildDynamic(MethodInfo method);
    }
}
