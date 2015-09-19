using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Weakly
{
    /// <summary>
    /// A collections which only holds weak references to the items.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public class WeakCollection<T> : ICollection<T>
        where T : class
    {
        private readonly List<WeakReference> _inner;
        private readonly WeakReference _gcSentinel = new WeakReference(new object());

        private void CleanIfNeeded()
        {
            if (_gcSentinel.IsAlive)
                return;

            _gcSentinel.Target = new object();
            Purge();
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakCollection&lt;T&gt;"/> class that is empty and has the default initial capacity.
        /// </summary>
        public WeakCollection()
        {
            _inner = new List<WeakReference>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakCollection&lt;T&gt;"/> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new collection.</param>
        public WeakCollection(IEnumerable<T> collection)
        {
            _inner = new List<WeakReference>(collection.Select(item => new WeakReference(item)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakCollection&lt;T&gt;"/> class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public WeakCollection(int capacity)
        {
            _inner = new List<WeakReference>(capacity);
        }

        #endregion

        /// <summary>
        /// Removes all dead entries.
        /// </summary>
        /// <returns>true if entries where removed; otherwise false.</returns>
        public bool Purge()
        {
            return _inner.RemoveAll(l => !l.IsAlive) > 0;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            CleanIfNeeded();

            var enumerable = _inner.Select(item => (T) item.Target)
                .Where(value => value != null);
            return enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public void Add(T item)
        {
            CleanIfNeeded();
            _inner.Add(new WeakReference(item));
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public void Clear()
        {
            _inner.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
        public bool Contains(T item)
        {
            CleanIfNeeded();
            return _inner.FindIndex(w => ((T) w.Target) == item) >= 0;
        }

        /// <summary>
        /// Copies the elements of the collection to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from the collection.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            CleanIfNeeded();

            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if ((arrayIndex + _inner.Count) > array.Length)
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");

            var items = _inner.Select(item => (T) item.Target)
                .Where(value => value != null)
                .ToArray();

            items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        public bool Remove(T item)
        {
            CleanIfNeeded();

            for (var i = 0; i < _inner.Count; i++)
            {
                var target = (T) _inner[i].Target;
                if (target == item)
                {
                    _inner.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        public int Count
        {
            get
            {
                CleanIfNeeded();
                return _inner.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
        public bool IsReadOnly
        {
            get { return false; }
        }
    }
}
