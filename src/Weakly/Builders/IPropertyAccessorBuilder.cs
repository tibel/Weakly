using System;
using System.Reflection;

namespace Weakly.Builders
{
    /// <summary>
    /// Creates property accessors.
    /// </summary>
    public interface IPropertyAccessorBuilder
    {
        /// <summary>
        /// Get compiled Getter function from a given <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The function to get the property value.</returns>
        Func<object, object> BuildGetter(PropertyInfo property);

        /// <summary>
        /// Get compiled Setter function from a given <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The function to set the property value.</returns>
        Action<object, object> BuildSetter(PropertyInfo property);
    }
}
