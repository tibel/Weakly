using System;
using System.Reflection;

namespace Weakly
{
    internal class CachingDynamicDelegateBuilderDecorator : IDynamicDelegateBuilder
    {
        private readonly IDynamicDelegateBuilder _builder;
        private readonly SimpleCache<MethodInfo, Func<object, object[], object>> _cache = new SimpleCache<MethodInfo, Func<object, object[], object>>();

        public CachingDynamicDelegateBuilderDecorator(IDynamicDelegateBuilder builder)
        {
            _builder = builder;
        }

        public Func<object, object[], object> BuildDynamic(MethodInfo method)
        {
            var action = _cache.GetValueOrDefault(method);
            if (action != null) return action;
            action = _builder.BuildDynamic(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }
    }
}
