using System;
using System.Collections.Generic;
using System.Reflection;

namespace Weakly
{
    internal sealed class GenericMethodCache<TDelegate>
        where TDelegate : class
    {
        private readonly IDictionary<Tuple<RuntimeMethodHandle, RuntimeTypeHandle>, TDelegate> _storage;

        public GenericMethodCache()
        {
            _storage = new Dictionary<Tuple<RuntimeMethodHandle, RuntimeTypeHandle>, TDelegate>();
        }

        private static Tuple<RuntimeMethodHandle, RuntimeTypeHandle> ToHandle(MethodBase methodInfo)
        {
            return new Tuple<RuntimeMethodHandle, RuntimeTypeHandle>(methodInfo.MethodHandle, methodInfo.DeclaringType.TypeHandle);
        }

        public TDelegate GetValueOrNull(MethodInfo key)
        {
            return GetValueOrNull(ToHandle(key));
        }

        public TDelegate GetValueOrNull(Tuple<RuntimeMethodHandle, RuntimeTypeHandle> key)
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
            AddOrReplace(ToHandle(key), value);
        }

        public void AddOrReplace(Tuple<RuntimeMethodHandle, RuntimeTypeHandle> key, TDelegate value)
        {
            lock (_storage)
            {
                _storage[key] = value;
            }
        }

        public bool Remove(MethodInfo key)
        {
            return Remove(ToHandle(key));
        }

        public bool Remove(Tuple<RuntimeMethodHandle, RuntimeTypeHandle> key)
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
