using System;
using System.Reflection;

namespace Weakly
{
    internal class CachingOpenFuncBuilderDecorator : IOpenFuncBuilder
    {
        private readonly IOpenFuncBuilder _builder;
        private readonly SimpleCache<MethodInfo, Delegate> _cache = new SimpleCache<MethodInfo, Delegate>();

        public CachingOpenFuncBuilderDecorator(IOpenFuncBuilder builder)
        {
            _builder = builder;
        }

        public Func<object, TResult> BuildFunc<TResult>(MethodInfo method)
        {
            var func = _cache.GetValueOrDefault<Func<object, TResult>>(method);
            if (func != null) return func;
            func = _builder.BuildFunc<TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }

        public Func<object, T, TResult> BuildFunc<T, TResult>(MethodInfo method)
        {
            var func = _cache.GetValueOrDefault<Func<object, T, TResult>>(method);
            if (func != null) return func;
            func = _builder.BuildFunc<T, TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }

        public Func<object, T1, T2, TResult> BuildFunc<T1, T2, TResult>(MethodInfo method)
        {
            var func = _cache.GetValueOrDefault<Func<object, T1, T2, TResult>>(method);
            if (func != null) return func;
            func = _builder.BuildFunc<T1, T2, TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }

        public Func<object, T1, T2, T3, TResult> BuildFunc<T1, T2, T3, TResult>(MethodInfo method)
        {
            var func = _cache.GetValueOrDefault<Func<object, T1, T2, T3, TResult>>(method);
            if (func != null) return func;
            func = _builder.BuildFunc<T1, T2, T3, TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }

        public Func<object, T1, T2, T3, T4, TResult> BuildFunc<T1, T2, T3, T4, TResult>(MethodInfo method)
        {
            var func = _cache.GetValueOrDefault<Func<object, T1, T2, T3, T4, TResult>>(method);
            if (func != null) return func;
            func = _builder.BuildFunc<T1, T2, T3, T4, TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }

        public Func<object, T1, T2, T3, T4, T5, TResult> BuildFunc<T1, T2, T3, T4, T5, TResult>(MethodInfo method)
        {
            var func = _cache.GetValueOrDefault<Func<object, T1, T2, T3, T4, T5, TResult>>(method);
            if (func != null) return func;
            func = _builder.BuildFunc<T1, T2, T3, T4, T5, TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }
    }
}
