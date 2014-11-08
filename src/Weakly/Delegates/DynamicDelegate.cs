using System;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper to create dynamic delegate functions.
    /// </summary>
    public static class DynamicDelegate
    {
        private static readonly IDynamicDelegateBuilder Builder = new CachingDynamicDelegateBuilderDecorator(new ExpressionDynamicDelegateBuilder());

        /// <summary>
        /// Create a dynamic delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The dynamic delegate.</returns>
        public static Func<object, object[], object> From(MethodInfo method)
        {
            return Builder.BuildDynamic(method);
        }
    }
}
