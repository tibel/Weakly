using System;
using System.Collections.Generic;
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
            var action = Cache.GetValueOrNull(property);
            if (action != null) return action;
            action = CompileGetter(property);
            Cache.AddOrReplace(property, action);
            return action;
        }

        private static Func<object, object> CompileGetter(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var typedInstance = Expression.Convert(instance, property.DeclaringType);
            var propertyValue = Expression.Property(typedInstance, property);
            var body = Expression.Convert(propertyValue, typeof(object));
            return Expression.Lambda<Func<object, object>>(body, instance).Compile();
        }

        #region Inner Types

        private static class Cache
        {
            private static readonly IDictionary<PropertyInfo, Func<object, object>> Storage =
                new Dictionary<PropertyInfo, Func<object, object>>();

            public static Func<object, object> GetValueOrNull(PropertyInfo key)
            {
                Func<object, object> func;
                lock (Storage)
                {
                    Storage.TryGetValue(key, out func);
                }
                return func;
            }

            public static void AddOrReplace(PropertyInfo key, Func<object, object> func)
            {
                lock (Storage)
                {
                    Storage[key] = func;
                }
            }
        }

        #endregion
    }
}
