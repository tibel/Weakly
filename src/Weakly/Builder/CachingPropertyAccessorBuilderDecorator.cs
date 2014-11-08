using System;
using System.Reflection;

namespace Weakly
{
    internal class CachingPropertyAccessorBuilderDecorator : IPropertyAccessorBuilder
    {
        private readonly IPropertyAccessorBuilder _builder;
        private readonly SimpleCache<PropertyInfo, Func<object, object>> _getterCache = new SimpleCache<PropertyInfo, Func<object, object>>();
        private readonly SimpleCache<PropertyInfo, Action<object, object>> _setterCache = new SimpleCache<PropertyInfo, Action<object, object>>();

        public CachingPropertyAccessorBuilderDecorator(IPropertyAccessorBuilder builder)
        {
            _builder = builder;
        }

        public Func<object, object> BuildGetter(PropertyInfo property)
        {
            var action = _getterCache.GetValueOrDefault(property);
            if (action != null) return action;
            action = _builder.BuildGetter(property);
            _getterCache.AddOrUpdate(property, action);
            return action;
        }

        public Action<object, object> BuildSetter(PropertyInfo property)
        {
            var action = _setterCache.GetValueOrDefault(property);
            if (action != null) return action;
            action = _builder.BuildSetter(property);
            _setterCache.AddOrUpdate(property, action);
            return action;
        }
    }
}
