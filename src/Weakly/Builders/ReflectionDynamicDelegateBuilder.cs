using System;
using System.Reflection;

namespace Weakly.Builders
{
    /// <summary>
    /// Reflection based <see cref="IDynamicDelegateBuilder"/>.
    /// </summary>
    public sealed class ReflectionDynamicDelegateBuilder : IDynamicDelegateBuilder
    {
        /// <summary>
        /// Create a dynamic delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The dynamic delegate.
        /// </returns>
        public Func<object, object[], object> BuildDynamic(MethodInfo method)
        {
            return method.Invoke;
        }
    }
}
