using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper to create dynamic (complied) property accessors.
    /// </summary>
    public static class DynamicProperty
    {
        /// <summary>
        /// Get compiled Getter function from a given <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The function to get the property value.</returns>
        public static Func<object, object> GetterFrom(PropertyInfo property)
        {
            var action = GetterCache.GetValueOrDefault(property);
            if (action != null) return action;
            action = CompileGetter(property);
            GetterCache.AddOrUpdate(property, action);
            return action;
        }

        private static Func<object, object> CompileGetter(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var typedInstance = Expression.Convert(instance, property.DeclaringType);
            var propertyMember = Expression.Property(typedInstance, property);
            var body = Expression.Convert(propertyMember, typeof(object));
            return Expression.Lambda<Func<object, object>>(body, instance).Compile();
        }

        /// <summary>
        /// Get compiled Setter function from a given <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The function to set the property value.</returns>
        public static Action<object, object> SetterFrom(PropertyInfo property)
        {
            var action = SetterCache.GetValueOrDefault(property);
            if (action != null) return action;
            action = CompileSetter(property);
            SetterCache.AddOrUpdate(property, action);
            return action;
        }

        private static Action<object, object> CompileSetter(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var value = Expression.Parameter(typeof(object), "value");
            var typedInstance = Expression.Convert(instance, property.DeclaringType);
            var typedValue = Expression.Convert(value, property.PropertyType);
            var propertyMember = Expression.Property(typedInstance, property);
            var body = Expression.Assign(propertyMember, typedValue);
            return Expression.Lambda<Action<object, object>>(body, instance, value).Compile();
        }

        private static readonly SimpleCache<PropertyInfo, Func<object, object>> GetterCache = new SimpleCache<PropertyInfo, Func<object, object>>();
        private static readonly SimpleCache<PropertyInfo, Action<object, object>> SetterCache = new SimpleCache<PropertyInfo, Action<object, object>>();
    }
}
