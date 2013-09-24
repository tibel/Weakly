using System;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Weak version of <see cref="Action"/> delegate.
    /// </summary>
    public sealed class WeakAction : WeakDelegate
    {
        private readonly Action<object> _openAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction"/> class.
        /// </summary>
        /// <param name="action">The action delegate to encapsulate.</param>
        public WeakAction(Action action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openAction = OpenAction.From(action.Method);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakAction(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openAction = OpenAction.From(method);
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        public void Invoke()
        {
            var target = Target;
            if (target != null)
                _openAction(target);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Action&lt;T&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T">The parameter of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakAction<T> : WeakDelegate
    {
        private readonly Action<object, T> _openAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="action">The action delegate to encapsulate.</param>
        public WeakAction(Action<T> action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openAction = OpenAction.From<T>(action.Method);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakAction(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openAction = OpenAction.From<T>(method);
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <param name="obj">The parameter of the method that this delegate encapsulates.</param>
        public void Invoke(T obj)
        {
            var target = Target;
            if (target != null)
                _openAction(target, obj);
        }
    }

    /// <summary>
    /// Weak version of <see cref="Action&lt;T1, T2&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakAction<T1, T2> : WeakDelegate
    {
        private readonly Action<object, T1, T2> _openAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2&gt;"/> class.
        /// </summary>
        /// <param name="action">The action delegate to encapsulate.</param>
        public WeakAction(Action<T1, T2> action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openAction = OpenAction.From<T1, T2>(action.Method);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakAction(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openAction = OpenAction.From<T1, T2>(method);
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
                _openAction(target, arg1, arg2);
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
        private readonly Action<object, T1, T2, T3> _openAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2, T3&gt;"/> class.
        /// </summary>
        /// <param name="action">The action delegate to encapsulate.</param>
        public WeakAction(Action<T1, T2, T3> action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openAction = OpenAction.From<T1, T2, T3>(action.Method);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;T1, T2, T3&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="method">The method represented by the delegate.</param>
        public WeakAction(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openAction = OpenAction.From<T1, T2, T3>(method);
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
                _openAction(target, arg1, arg2, arg3);
        }
    }
}
