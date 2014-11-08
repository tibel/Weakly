using System;
using System.Linq.Expressions;

namespace Weakly.Builders
{
    /// <summary>
    /// <see cref="Expression"/> based <see cref="IPropertyAccessorBuilder"/>.
    /// </summary>
    public class ExpressionPropertyAccessorBuilder : IPropertyAccessorBuilder
    {
        /// <summary>
        /// Get compiled Getter function from a given <paramref name="property" />.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        /// The function to get the property value.
        /// </returns>
        public Func<object, object> BuildGetter(System.Reflection.PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var typedInstance = Expression.Convert(instance, property.DeclaringType);
            var propertyMember = Expression.Property(typedInstance, property);
            var body = Expression.Convert(propertyMember, typeof(object));
            return Expression.Lambda<Func<object, object>>(body, instance).Compile();
        }

        /// <summary>
        /// Get compiled Setter function from a given <paramref name="property" />.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        /// The function to set the property value.
        /// </returns>
        public Action<object, object> BuildSetter(System.Reflection.PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var value = Expression.Parameter(typeof(object), "value");
            var typedInstance = Expression.Convert(instance, property.DeclaringType);
            var typedValue = Expression.Convert(value, property.PropertyType);
            var propertyMember = Expression.Property(typedInstance, property);
            var body = Expression.Assign(propertyMember, typedValue);
            return Expression.Lambda<Action<object, object>>(body, instance, value).Compile();
        }
    }
}
