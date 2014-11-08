using System;
using System.Reflection;

namespace Weakly.Builders
{
    /// <summary>
    /// Creates dynamic delegate functions.
    /// </summary>
    public interface IDynamicDelegateBuilder
    {
        /// <summary>
        /// Create a dynamic delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The dynamic delegate.</returns>
        Func<object, object[], object> BuildDynamic(MethodInfo method);
    }
}
