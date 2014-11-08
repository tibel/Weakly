using System;
using System.Reflection;
using Weakly.Builders;

namespace Weakly
{
    /// <summary>
    /// Weak version of <see cref="Func&lt;TResult&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakFunc<TResult> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;TResult&gt;"/> class.
        /// </summary>
        /// <param name="function">The function delegate to encapsulate.</param>
        public WeakFunc(Func<TResult> function)
            : this(function.Target, function.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;TResult&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakFunc(object target, MethodInfo method)
            : base(target, method)
        {
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <returns>The return value of the method that this delegate encapsulates.</returns>
        public TResult Invoke()
        {
            var target = Target;
            if (target != null)
                return Builder.OpenFunc.BuildFunc<TResult>(Method)(target);
            return default(TResult);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Func&lt;T, TResult&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakFunc<T, TResult> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;T, TResult&gt;"/> class.
        /// </summary>
        /// <param name="function">The function delegate to encapsulate.</param>
        public WeakFunc(Func<T, TResult> function)
            : this(function.Target, function.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;T, TResult&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakFunc(object target, MethodInfo method)
            : base(target, method)
        {
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <param name="obj">The parameter of the method that this delegate encapsulates.</param>
        /// <returns>The return value of the method that this delegate encapsulates.</returns>
        public TResult Invoke(T obj)
        {
            var target = Target;
            if (target != null)
                return Builder.OpenFunc.BuildFunc<T, TResult>(Method)(target, obj);
            return default(TResult);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Func&lt;T1, T2, TResult&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakFunc<T1, T2, TResult> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;T1, T2, TResult&gt;"/> class.
        /// </summary>
        /// <param name="function">The function delegate to encapsulate.</param>
        public WeakFunc(Func<T1, T2, TResult> function)
            : this(function.Target, function.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;T1, T2, TResult&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakFunc(object target, MethodInfo method)
            : base(target, method)
        {
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <returns>The return value of the method that this delegate encapsulates.</returns>
        public TResult Invoke(T1 arg1, T2 arg2)
        {
            var target = Target;
            if (target != null)
                return Builder.OpenFunc.BuildFunc<T1, T2, TResult>(Method)(target, arg1, arg2);
            return default(TResult);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Func&lt;T1, T2, T3, TResult&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakFunc<T1, T2, T3, TResult> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;T1, T2, T3, TResult&gt;"/> class.
        /// </summary>
        /// <param name="function">The function delegate to encapsulate.</param>
        public WeakFunc(Func<T1, T2, T3, TResult> function)
            : this(function.Target, function.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;T1, T2, T3, TResult&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakFunc(object target, MethodInfo method)
            : base(target, method)
        {
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <returns>The return value of the method that this delegate encapsulates.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            var target = Target;
            if (target != null)
                return Builder.OpenFunc.BuildFunc<T1, T2, T3, TResult>(Method)(target, arg1, arg2, arg3);
            return default(TResult);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Func&lt;T1, T2, T3, T4, TResult&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakFunc<T1, T2, T3, T4, TResult> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;T1, T2, T3, T4, TResult&gt;"/> class.
        /// </summary>
        /// <param name="function">The function delegate to encapsulate.</param>
        public WeakFunc(Func<T1, T2, T3, T4, TResult> function)
            : this(function.Target, function.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;T1, T2, T3, T4, TResult&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakFunc(object target, MethodInfo method)
            : base(target, method)
        {
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <returns>The return value of the method that this delegate encapsulates.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var target = Target;
            if (target != null)
                return Builder.OpenFunc.BuildFunc<T1, T2, T3, T4, TResult>(Method)(target, arg1, arg2, arg3, arg4);
            return default(TResult);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Func&lt;T1, T2, T3, T4, T5, TResult&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakFunc<T1, T2, T3, T4, T5, TResult> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;T1, T2, T3, T4, T5, TResult&gt;"/> class.
        /// </summary>
        /// <param name="function">The function delegate to encapsulate.</param>
        public WeakFunc(Func<T1, T2, T3, T4, T5, TResult> function)
            : this(function.Target, function.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;T1, T2, T3, T4, T5, TResult&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakFunc(object target, MethodInfo method)
            : base(target, method)
        {
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <returns>The return value of the method that this delegate encapsulates.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var target = Target;
            if (target != null)
                return Builder.OpenFunc.BuildFunc<T1, T2, T3, T4, T5, TResult>(Method)(target, arg1, arg2, arg3, arg4, arg5);
            return default(TResult);
        }
    }
}
