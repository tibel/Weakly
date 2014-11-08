using System;
using System.Reflection;
using Weakly.Builders;

namespace Weakly
{
    /// <summary>
    /// Helper to create dynamic delegate functions.
    /// </summary>
    public static class DynamicDelegate
    {
        /// <summary>
        /// Create a dynamic delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The dynamic delegate.</returns>
        public static Func<object, object[], object> From(MethodInfo method)
        {
            return Builder.DynamicDelegate.BuildDynamic(method);
        }
    }
}
