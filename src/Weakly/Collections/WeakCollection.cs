using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Weakly
{
    public class WeakCollection<T> : ICollection<T>
        where T : class
    {
        private readonly List<WeakReference<T>> _inner;
        private WeakReference _gcSentinel = new WeakReference(new object());

        #region Cleanup handling

        private bool IsCleanupNeeded()
        {
            if (_gcSentinel.Target == null)
            {
                _gcSentinel = new WeakReference(new object());
                return true;
            }

            return false;
        }

        private void CleanAbandonedItems()
        {
            for (var i = _inner.Count - 1; i >= 0; i--)
            {
                var entry = _inner[i];
                if (!entry.IsAlive)
                    _inner.RemoveAt(i);
            }
        }

        private void CleanIfNeeded()
        {
            if (IsCleanupNeeded())
            {
                CleanAbandonedItems();
            }
        }

        #endregion

        #region Constructors

        public WeakCollection()
        {
            _inner = new List<WeakReference<T>>();
        }

        public WeakCollection(IEnumerable<T> collection)
        {
            _inner = new List<WeakReference<T>>();

            foreach (var item in collection)
            {
                _inner.Add(new WeakReference<T>(item));
            }
        }

        public WeakCollection(int capacity)
        {
            _inner = new List<WeakReference<T>>(capacity);
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            CleanIfNeeded();
            var enumerable = _inner.Select(value => value.Target)
                .Where(value => value != null);
            return enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            CleanIfNeeded();
            _inner.Add(new WeakReference<T>(item));
        }

        public void Clear()
        {
            _inner.Clear();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                CleanIfNeeded();
                return _inner.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
    }
}
