using System;
using System.Reflection;

namespace Weakly
{
    public sealed class WeakFunc<TResult> : WeakDelegate
    {
        private readonly Func<object, TResult> _openFunc;

        public WeakFunc(Func<TResult> action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openFunc = OpenFunc.From<TResult>(action.Method);
        }

        public WeakFunc(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openFunc = OpenFunc.From<TResult>(method);
        }

        public TResult Invoke()
        {
            var target = Target;
            if (target != null)
                return _openFunc(target);
            return default(TResult);
        }
    }

    public sealed class WeakFunc<T, TResult> : WeakDelegate
    {
        private readonly Func<object, T, TResult> _openFunc;

        public WeakFunc(Func<T, TResult> action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openFunc = OpenFunc.From<T, TResult>(action.Method);
        }

        public WeakFunc(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openFunc = OpenFunc.From<T, TResult>(method);
        }

        public TResult Invoke(T obj)
        {
            var target = Target;
            if (target != null)
                return _openFunc(target, obj);
            return default(TResult);
        }
    }

    public sealed class WeakFunc<T1, T2, TResult> : WeakDelegate
    {
        private readonly Func<object, T1, T2, TResult> _openFunc;

        public WeakFunc(Func<T1, T2, TResult> action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openFunc = OpenFunc.From<T1, T2, TResult>(action.Method);
        }

        public WeakFunc(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openFunc = OpenFunc.From<T1, T2, TResult>(method);
        }

        public TResult Invoke(T1 arg1, T2 arg2)
        {
            var target = Target;
            if (target != null)
                return _openFunc(target, arg1, arg2);
            return default(TResult);
        }
    }

    public sealed class WeakFunc<T1, T2, T3, TResult> : WeakDelegate
    {
        private readonly Func<object, T1, T2, T3, TResult> _openFunc;

        public WeakFunc(Func<T1, T2, T3, TResult> action)
            : base(action.Target, action.Method.MethodHandle)
        {
            _openFunc = OpenFunc.From<T1, T2, T3, TResult>(action.Method);
        }

        public WeakFunc(object target, MethodInfo method)
            : base(target, method.MethodHandle)
        {
            _openFunc = OpenFunc.From<T1, T2, T3, TResult>(method);
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            var target = Target;
            if (target != null)
                return _openFunc(target, arg1, arg2, arg3);
            return default(TResult);
        }
    }
}
