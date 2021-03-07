using System;
using System.Reflection;

namespace Weakly.Builders
{
    /// <summary>
    /// Caching decorator for <see cref="IPropertyAccessorBuilder"/>.
    /// </summary>
    public sealed class CachingPropertyAccessorBuilderDecorator : IPropertyAccessorBuilder
    {
        private readonly IPropertyAccessorBuilder _builder;
        private readonly SimpleCache<PropertyInfo, Func<object, object>> _getterCache = new SimpleCache<PropertyInfo, Func<object, object>>();
        private readonly SimpleCache<PropertyInfo, Action<object, object>> _setterCache = new SimpleCache<PropertyInfo, Action<object, object>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingPropertyAccessorBuilderDecorator"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public CachingPropertyAccessorBuilderDecorator(IPropertyAccessorBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Get compiled Getter function from a given <paramref name="property" />.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        /// The function to get the property value.
        /// </returns>
        public Func<object, object> BuildGetter(PropertyInfo property)
        {
            var action = _getterCache.GetValueOrDefault(property);
            if (action is object) return action;
            action = _builder.BuildGetter(property);
            _getterCache.AddOrUpdate(property, action);
            return action;
        }

        /// <summary>
        /// Get compiled Setter function from a given <paramref name="property" />.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        /// The function to set the property value.
        /// </returns>
        public Action<object, object> BuildSetter(PropertyInfo property)
        {
            var action = _setterCache.GetValueOrDefault(property);
            if (action is object) return action;
            action = _builder.BuildSetter(property);
            _setterCache.AddOrUpdate(property, action);
            return action;
        }
    }
}
