using System;

namespace Weakly
{
    /// <summary>
    /// Weak version of <see cref="Action"/> delegate.
    /// </summary>
    /// <typeparam name="TTarget">The type of the referenced action target.</typeparam>
    public sealed class WeakAction<TTarget>
        where TTarget : class
    {
        private readonly WeakReference<TTarget> _target;
        private readonly Action<TTarget> _weakAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction&lt;TTarget&gt;"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="weakAction">The method represented by the delegate.</param>
        public WeakAction(TTarget target, [EmptyCapture] Action<TTarget> weakAction)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (weakAction == null)
                throw new ArgumentNullException(nameof(weakAction));

            _target = new WeakReference<TTarget>(target);
            _weakAction = weakAction;
        }

        /// <summary>
        /// Invokes the method represented by the current weak delegate.
        /// </summary>
        public void Invoke()
        {
            TTarget target;
            if (_target.TryGetTarget(out target))
            {
                _weakAction(target);
            }
        }
    }
}
