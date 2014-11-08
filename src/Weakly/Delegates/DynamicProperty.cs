using System;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper to create dynamic (complied) property accessors.
    /// </summary>
    public static class DynamicProperty
    {
        private static readonly IPropertyAccessorBuilder Builder = new CachingPropertyAccessorBuilderDecorator(new ExpressionPropertyAccessorBuilder());

        /// <summary>
        /// Get compiled Getter function from a given <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The function to get the property value.</returns>
        public static Func<object, object> GetterFrom(PropertyInfo property)
        {
            return Builder.BuildGetter(property);
        }

        /// <summary>
        /// Get compiled Setter function from a given <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The function to set the property value.</returns>
        public static Action<object, object> SetterFrom(PropertyInfo property)
        {
            return Builder.BuildSetter(property);
        }
    }
}
