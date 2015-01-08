using System;

namespace Weakly.Builders
{
    /// <summary>
    /// Reflection based <see cref="IPropertyAccessorBuilder"/>.
    /// </summary>
    public sealed class ReflectionPropertyAccessorBuilder : IPropertyAccessorBuilder
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
            return property.GetValue;
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
            return property.SetValue;
        }
    }
}
