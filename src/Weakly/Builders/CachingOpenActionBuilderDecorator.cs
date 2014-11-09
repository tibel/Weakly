using System;
using System.Reflection;

namespace Weakly.Builders
{
    /// <summary>
    /// Caching decorator for <see cref="IOpenActionBuilder"/>.
    /// </summary>
    public sealed class CachingOpenActionBuilderDecorator : IOpenActionBuilder
    {
        private readonly SimpleCache<MethodInfo, Delegate> _cache = new SimpleCache<MethodInfo, Delegate>();
        private readonly IOpenActionBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingOpenActionBuilderDecorator"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public CachingOpenActionBuilderDecorator(IOpenActionBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Action<object> BuildAction(MethodInfo method)
        {
            var action = (Action<object>) _cache.GetValueOrDefault(method);
            if (action != null) return action;
            action = _builder.BuildAction(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Action<object, T> BuildAction<T>(MethodInfo method)
        {
            var action = (Action<object, T>) _cache.GetValueOrDefault(method);
            if (action != null) return action;
            action = _builder.BuildAction<T>(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Action<object, T1, T2> BuildAction<T1, T2>(MethodInfo method)
        {
            var action = (Action<object, T1, T2>) _cache.GetValueOrDefault(method);
            if (action != null) return action;
            action = _builder.BuildAction<T1, T2>(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Action<object, T1, T2, T3> BuildAction<T1, T2, T3>(MethodInfo method)
        {
            var action = (Action<object, T1, T2, T3>) _cache.GetValueOrDefault(method);
            if (action != null) return action;
            action = _builder.BuildAction<T1, T2, T3>(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Action<object, T1, T2, T3, T4> BuildAction<T1, T2, T3, T4>(MethodInfo method)
        {
            var action = (Action<object, T1, T2, T3, T4>) _cache.GetValueOrDefault(method);
            if (action != null) return action;
            action = _builder.BuildAction<T1, T2, T3, T4>(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }

        /// <summary>
        /// Create an open delegate from the specified method.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The open delegate.
        /// </returns>
        public Action<object, T1, T2, T3, T4, T5> BuildAction<T1, T2, T3, T4, T5>(MethodInfo method)
        {
            var action = (Action<object, T1, T2, T3, T4, T5>) _cache.GetValueOrDefault(method);
            if (action != null) return action;
            action = _builder.BuildAction<T1, T2, T3, T4, T5>(method);
            _cache.AddOrUpdate(method, action);
            return action;
        }
    }
}
