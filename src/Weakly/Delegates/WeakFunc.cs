using System;

namespace Weakly
{
    /// <summary>
    /// Weak version of <see cref="Func&lt;TResult&gt;"/> delegate.
    /// </summary>
    /// <typeparam name="TTarget">The type of the referenced function target.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    public sealed class WeakFunc<TTarget, TResult>
        where TTarget : class
    {
        private readonly WeakReference<TTarget> _target;
        private readonly Func<TTarget, TResult> _weakFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFunc&lt;TTarget, TResult&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="weakFunc">The method represented by the delegate.</param>
        public WeakFunc(TTarget target, [EmptyCapture] Func<TTarget, TResult> weakFunc)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (weakFunc == null)
                throw new ArgumentNullException(nameof(weakFunc));

            _target = new WeakReference<TTarget>(target);
            _weakFunc = weakFunc;
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        /// <returns>The return value of the method that this delegate encapsulates.</returns>
        public TResult Invoke()
        {
            TTarget target;
            if (_target.TryGetTarget(out target))
            {
                return _weakFunc(target);
            }

            return default(TResult);
        }
    }
}
