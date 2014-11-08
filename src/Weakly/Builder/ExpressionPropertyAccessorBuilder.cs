using System;
using System.Linq.Expressions;

namespace Weakly
{
    internal class ExpressionPropertyAccessorBuilder : IPropertyAccessorBuilder
    {
        public Func<object, object> BuildGetter(System.Reflection.PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var typedInstance = Expression.Convert(instance, property.DeclaringType);
            var propertyMember = Expression.Property(typedInstance, property);
            var body = Expression.Convert(propertyMember, typeof(object));
            return Expression.Lambda<Func<object, object>>(body, instance).Compile();
        }

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
