using System;
using System.Collections.Generic;

namespace Weakly
{
    /// <summary>
    /// Helper for all open delegates.
    /// </summary>
    public static class OpenDelegate
    {
        internal static class Cache
        {
            private static readonly IDictionary<RuntimeMethodHandle, Delegate> Storage = new Dictionary<RuntimeMethodHandle, Delegate>();

            public static TDelegate GetValueOrNull<TDelegate>(RuntimeMethodHandle key)
                where TDelegate : class
            {
                Delegate d;
                lock (Storage)
                {
                    Storage.TryGetValue(key, out d);
                }
                return d as TDelegate;
            }

            public static void AddOrReplace(RuntimeMethodHandle key, Delegate value)
            {
                lock (Storage)
                {
                    Storage[key] = value;
                }
            }   
        }
    }
}
