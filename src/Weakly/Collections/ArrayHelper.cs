using System;

namespace Weakly
{
    internal static class ArrayHelper
    {
#if NETSTANDARD1_0
        private static class Array<T>
        {
            public static readonly T[] Empty = new T[0];
        }

        public static T[] Empty<T>()
        {
            return Array<T>.Empty;
        }
#else
        public static T[] Empty<T>()
        {
            return Array.Empty<T>();
        }
#endif
    }
}
