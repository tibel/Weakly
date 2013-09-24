using System;

namespace Weakly
{
    /// <summary>
    /// Base class for all weak delegates.
    /// </summary>
    public abstract class WeakDelegate
    {
        private readonly WeakReference _instance;
        private readonly RuntimeMethodHandle _methodHandle;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakDelegate"/> class.
        /// </summary>
        /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
        /// <param name="methodHandle">The method handle represented by the delegate.</param>
        protected WeakDelegate(object target, RuntimeMethodHandle methodHandle)
        {
            _instance = new WeakReference(target);
            _methodHandle = methodHandle;
        }

        /// <summary>
        /// Gets an indication whether the object referenced by the current <see cref="WeakDelegate"/> object has been garbage collected.
        /// </summary>
        public bool IsAlive
        {
            get { return _instance.IsAlive; }
        }

        /// <summary>
        /// Gets the class instance on which the current <see cref="WeakDelegate"/> invokes the instance method.
        /// </summary>
        public object Target
        {
            get { return _instance.Target; }
        }

        /// <summary>
        /// Gets the method handle represented by this delegate.
        /// </summary>
        public RuntimeMethodHandle MethodHandle
        {
            get { return _methodHandle; }
        }
    }
}
