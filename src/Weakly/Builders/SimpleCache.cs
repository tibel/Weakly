using System.Collections.Generic;

namespace Weakly.Builders
{
    internal sealed class SimpleCache<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _storage;

        public SimpleCache()
        {
            _storage = new Dictionary<TKey, TValue>();
        }

        public TValue GetValueOrDefault(TKey key)
        {
            TValue value;
            lock (_storage)
            {
                _storage.TryGetValue(key, out value);
            }
            return value;
        }

        public TValueAs GetValueOrDefault<TValueAs>(TKey key)
            where TValueAs : class
        {
            return GetValueOrDefault(key) as TValueAs;
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            lock (_storage)
            {
                _storage[key] = value;
            }
        }

        public bool Remove(TKey key)
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
}
