using System;
using System.Collections.Generic;
using System.Reflection;

namespace Weakly
{
    internal sealed class GenericMethodCache<TDelegate>
        where TDelegate : class
    {
        private readonly IDictionary<MethodInfo, TDelegate> _storage;

        public GenericMethodCache()
        {
            _storage = new Dictionary<MethodInfo, TDelegate>();
        }

        public TDelegate GetValueOrNull(MethodInfo key)
        {
            TDelegate value;
            lock (_storage)
            {
                _storage.TryGetValue(key, out value);
            }
            return value;
        }

        public void AddOrReplace(MethodInfo key, TDelegate value)
        {
            lock (_storage)
            {
                _storage[key] = value;
            }
        }

        public bool Remove(MethodInfo key)
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
        public static TDelegate GetValueOrNull<TDelegate>(this GenericMethodCache<Delegate> cache, MethodInfo key)
            where TDelegate : class
        {
            return cache.GetValueOrNull(key) as TDelegate;
        }
    }
}
