using System;
using System.Reflection;
using Weakly.Builders;

namespace Weakly
{
    /// <summary>
    /// Helper to create dynamic (complied) property accessors.
    /// </summary>
    [Obsolete("Use Builder.PropertyAccessor")]
    public static class DynamicProperty
    {
        /// <summary>
        /// Get compiled Getter function from a given <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The function to get the property value.</returns>
        public static Func<object, object> GetterFrom(PropertyInfo property)
        {
            return Builder.PropertyAccessor.BuildGetter(property);
        }

        /// <summary>
        /// Get compiled Setter function from a given <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The function to set the property value.</returns>
        public static Action<object, object> SetterFrom(PropertyInfo property)
        {
            return Builder.PropertyAccessor.BuildSetter(property);
        }
    }
}
