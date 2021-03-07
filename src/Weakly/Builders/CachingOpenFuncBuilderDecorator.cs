using System;
using System.Reflection;

namespace Weakly.Builders
{
    /// <summary>
    /// Caching decorator for <see cref="IOpenFuncBuilder"/>.
    /// </summary>
    public sealed class CachingOpenFuncBuilderDecorator : IOpenFuncBuilder
    {
        private readonly IOpenFuncBuilder _builder;
        private readonly SimpleCache<MethodInfo, Delegate> _cache = new SimpleCache<MethodInfo, Delegate>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingOpenFuncBuilderDecorator"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public CachingOpenFuncBuilderDecorator(IOpenFuncBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Func<object, TResult> BuildFunc<TResult>(MethodInfo method)
        {
            var func = (Func<object, TResult>) _cache.GetValueOrDefault(method);
            if (func is object) return func;
            func = _builder.BuildFunc<TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Func<object, T, TResult> BuildFunc<T, TResult>(MethodInfo method)
        {
            var func = (Func<object, T, TResult>) _cache.GetValueOrDefault(method);
            if (func is object) return func;
            func = _builder.BuildFunc<T, TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Func<object, T1, T2, TResult> BuildFunc<T1, T2, TResult>(MethodInfo method)
        {
            var func = (Func<object, T1, T2, TResult>) _cache.GetValueOrDefault(method);
            if (func is object) return func;
            func = _builder.BuildFunc<T1, T2, TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate
        /// </returns>
        public Func<object, T1, T2, T3, TResult> BuildFunc<T1, T2, T3, TResult>(MethodInfo method)
        {
            var func = (Func<object, T1, T2, T3, TResult>) _cache.GetValueOrDefault(method);
            if (func is object) return func;
            func = _builder.BuildFunc<T1, T2, T3, TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate
        /// </returns>
        public Func<object, T1, T2, T3, T4, TResult> BuildFunc<T1, T2, T3, T4, TResult>(MethodInfo method)
        {
            var func = (Func<object, T1, T2, T3, T4, TResult>) _cache.GetValueOrDefault(method);
            if (func is object) return func;
            func = _builder.BuildFunc<T1, T2, T3, T4, TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate
        /// </returns>
        public Func<object, T1, T2, T3, T4, T5, TResult> BuildFunc<T1, T2, T3, T4, T5, TResult>(MethodInfo method)
        {
            var func = (Func<object, T1, T2, T3, T4, T5, TResult>) _cache.GetValueOrDefault(method);
            if (func is object) return func;
            func = _builder.BuildFunc<T1, T2, T3, T4, T5, TResult>(method);
            _cache.AddOrUpdate(method, func);
            return func;
        }
    }
}
