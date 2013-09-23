using System;
using System.Reflection;

namespace Weakly
{
    public sealed class WeakAction : WeakDelegate
    {
        private readonly Action<object> _openAction;

        public WeakAction(Action action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openAction = OpenAction.From(action.Method);
        }

        public WeakAction(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openAction = OpenAction.From(method);
        }

        public void Invoke()
        {
            var target = Target;
            if (target != null)
                _openAction(target);
        }
    }

    public sealed class WeakAction<T> : WeakDelegate
    {
        private readonly Action<object, T> _openAction;

        public WeakAction(Action<T> action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openAction = OpenAction.From<T>(action.Method);
        }

        public WeakAction(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openAction = OpenAction.From<T>(method);
        }

        public void Invoke(T obj)
        {
            var target = Target;
            if (target != null)
                _openAction(target, obj);
        }
    }

    public sealed class WeakAction<T1, T2> : WeakDelegate
    {
        private readonly Action<object, T1, T2> _openAction;

        public WeakAction(Action<T1, T2> action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openAction = OpenAction.From<T1, T2>(action.Method);
        }

        public WeakAction(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openAction = OpenAction.From<T1, T2>(method);
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            var target = Target;
            if (target != null)
                _openAction(target, arg1, arg2);
        }
    }

    public sealed class WeakAction<T1, T2, T3> : WeakDelegate
    {
        private readonly Action<object, T1, T2, T3> _openAction;

        public WeakAction(Action<T1, T2, T3> action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openAction = OpenAction.From<T1, T2, T3>(action.Method);
        }

        public WeakAction(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openAction = OpenAction.From<T1, T2, T3>(method);
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            var target = Target;
            if (target != null)
                _openAction(target, arg1, arg2, arg3);
        }
    }
}
