﻿using System;
using System.Reflection;
using Weakly.Builders;

namespace Weakly
{
    /// <summary>
    /// Weak version of <see cref="Action"/> delegate.
    /// </summary>
    public sealed class WeakAction : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction"/> class.
        /// </summary>
        /// <param name="action">The action delegate to encapsulate.</param>
        public WeakAction(Action action)
            : this(action.Target, action.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakAction(object target, MethodInfo method)
            : base(target, method)
        {
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        public void Invoke()
        {
            var target = Target;
            if (target != null)
                Builder.OpenAction.BuildAction(Method)(target);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Action&lt;T&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T">The parameter of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakAction<T> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="action">The action delegate to encapsulate.</param>
        public WeakAction(Action<T> action)
            : this(action.Target, action.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakAction(object target, MethodInfo method)
            : base(target, method)
        {
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <param name="obj">The parameter of the method that this delegate encapsulates.</param>
        public void Invoke(T obj)
        {
            var target = Target;
            if (target != null)
                Builder.OpenAction.BuildAction<T>(Method)(target, obj);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Action&lt;T1, T2&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakAction<T1, T2> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2&gt;"/> class.
        /// </summary>
        /// <param name="action">The action delegate to encapsulate.</param>
        public WeakAction(Action<T1, T2> action)
            : this(action.Target, action.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakAction(object target, MethodInfo method)
            : base(target, method)
        {
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        public void Invoke(T1 arg1, T2 arg2)
        {
            var target = Target;
            if (target != null)
                Builder.OpenAction.BuildAction<T1, T2>(Method)(target, arg1, arg2);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Action&lt;T1, T2, T3&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakAction<T1, T2, T3> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2, T3&gt;"/> class.
        /// </summary>
        /// <param name="action">The action delegate to encapsulate.</param>
        public WeakAction(Action<T1, T2, T3> action)
            : this(action.Target, action.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2, T3&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakAction(object target, MethodInfo method)
            : base(target, method)
        {
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            var target = Target;
            if (target != null)
                Builder.OpenAction.BuildAction<T1, T2, T3>(Method)(target, arg1, arg2, arg3);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Action&lt;T1, T2, T3, T4&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakAction<T1, T2, T3, T4> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2, T3, T4&gt;"/> class.
        /// </summary>
        /// <param name="action">The action delegate to encapsulate.</param>
        public WeakAction(Action<T1, T2, T3, T4> action)
            : this(action.Target, action.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2, T3, T4&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakAction(object target, MethodInfo method)
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
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var target = Target;
            if (target != null)
                Builder.OpenAction.BuildAction<T1, T2, T3, T4>(Method)(target, arg1, arg2, arg3, arg4);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Action&lt;T1, T2, T3, T4, T5&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakAction<T1, T2, T3, T4, T5> : WeakDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2, T3, T4, T5&gt;"/> class.
        /// </summary>
        /// <param name="action">The action delegate to encapsulate.</param>
        public WeakAction(Action<T1, T2, T3, T4, T5> action)
            : this(action.Target, action.GetMethodInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2, T3, T4, T5&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakAction(object target, MethodInfo method)
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
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var target = Target;
            if (target != null)
                Builder.OpenAction.BuildAction<T1, T2, T3, T4, T5>(Method)(target, arg1, arg2, arg3, arg4, arg5);
        }
    }
}
