using System;
using System.Reflection;

namespace Weakly.Builders
{
    /// <summary>
    /// Caching decorator for <see cref="IDynamicDelegateBuilder"/>.
    /// </summary>
    public sealed class CachingDynamicDelegateBuilderDecorator : IDynamicDelegateBuilder
    {
        private readonly IDynamicDelegateBuilder _builder;
        private readonly SimpleCache<MethodInfo, Func<object, object[], object>> _cache = new SimpleCache<MethodInfo, Func<object, object[], object>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingDynamicDelegateBuilderDecorator"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public CachingDynamicDelegateBuilderDecorator(IDynamicDelegateBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Create a dynamic delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The dynamic delegate.
        /// </returns>
        public Func<object, object[], object> BuildDynamic(MethodInfo method)
        {
            var action = _cache.GetValueOrDefault(method);
            if (action != null) return action;
            action = _builder.BuildDynamic(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }
    }
}
