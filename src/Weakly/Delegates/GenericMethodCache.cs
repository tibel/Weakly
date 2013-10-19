using System;
using System.Collections.Generic;

namespace Weakly
{
    internal sealed class GenericMethodCache<TDelegate>
        where TDelegate : class
    {
        private readonly IDictionary<RuntimeMethodHandle, TDelegate> _storage;

        public GenericMethodCache()
        {
            _storage = new Dictionary<RuntimeMethodHandle, TDelegate>();
        }

        public TDelegate GetValueOrNull(RuntimeMethodHandle key)
        {
            TDelegate value;
            lock (_storage)
            {
                _storage.TryGetValue(key, out value);
            }
            return value;
        }

        public void AddOrReplace(RuntimeMethodHandle key, TDelegate value)
        {
            lock (_storage)
            {
                _storage[key] = value;
            }
        }

        public bool Remove(RuntimeMethodHandle key)
        {
            lock (_storage)
            {
                return _storage.Remove(key);
            }
        }

        public void Clear()
        {
            lock (_storage)
            {
                _storage.Clear();
            }
        }
    }

    internal static class GenericMethodCacheExtensions
    {
        public static TDelegate GetValueOrNull<TDelegate>(this GenericMethodCache<Delegate> cache, RuntimeMethodHandle key)
            where TDelegate : class
        {
            return cache.GetValueOrNull(key) as TDelegate;
        }
    }
}
