using System;
using System.Reflection;

namespace Weakly
{
    internal class CachingOpenActionBuilderDecorator : IOpenActionBuilder
    {
        private readonly SimpleCache<MethodInfo, Delegate> _cache = new SimpleCache<MethodInfo, Delegate>();
        private readonly IOpenActionBuilder _builder;
        
        public CachingOpenActionBuilderDecorator(IOpenActionBuilder builder)
        {
            _builder = builder;
        }

        public Action<object> BuildAction(MethodInfo method)
        {
            var action = _cache.GetValueOrDefault<Action<object>>(method);
            if (action != null) return action;
            action = _builder.BuildAction(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }

        public Action<object, T> BuildAction<T>(MethodInfo method)
        {
            var action = _cache.GetValueOrDefault<Action<object, T>>(method);
            if (action != null) return action;
            action = _builder.BuildAction<T>(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }

        public Action<object, T1, T2> BuildAction<T1, T2>(MethodInfo method)
        {
            var action = _cache.GetValueOrDefault<Action<object, T1, T2>>(method);
            if (action != null) return action;
            action = _builder.BuildAction<T1, T2>(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }

        public Action<object, T1, T2, T3> BuildAction<T1, T2, T3>(MethodInfo method)
        {
            var action = _cache.GetValueOrDefault<Action<object, T1, T2, T3>>(method);
            if (action != null) return action;
            action = _builder.BuildAction<T1, T2, T3>(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }

        public Action<object, T1, T2, T3, T4> BuildAction<T1, T2, T3, T4>(MethodInfo method)
        {
            var action = _cache.GetValueOrDefault<Action<object, T1, T2, T3, T4>>(method);
            if (action != null) return action;
            action = _builder.BuildAction<T1, T2, T3, T4>(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }

        public Action<object, T1, T2, T3, T4, T5> BuildAction<T1, T2, T3, T4, T5>(MethodInfo method)
        {
            var action = _cache.GetValueOrDefault<Action<object, T1, T2, T3, T4, T5>>(method);
            if (action != null) return action;
            action = _builder.BuildAction<T1, T2, T3, T4, T5>(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }
    }
}
