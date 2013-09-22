using System;

namespace Weakly
{
    /// <summary>
    /// Represents a weak reference, which references an object while still allowing that object to be reclaimed by garbage collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WeakReference<T> : WeakReference
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReference&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="target">An object to track.</param>
        public WeakReference(T target) : base(target)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReference&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="target">An object to track.</param>
        /// <param name="trackResurrection">Indicates when to stop tracking the object. If true, the object is tracked after finalization; if false, the object is only tracked until finalization.</param>
        public WeakReference(T target, bool trackResurrection) : base(target, trackResurrection)
        {
        }

        /// <summary>
        /// Gets or sets the object (the target) referenced by the current <see cref="WeakReference&lt;T&gt;" /> object.
        /// </summary>
        public new T Target
        {
            get { return (T) base.Target; }
            set { base.Target = value; }
        }
    }
}
