using System;
using System.Collections.Generic;

namespace Weakly
{
    /// <summary>
    /// Common extensions to <see cref="IEnumerable&lt;T&gt;"/>
    /// </summary>
    public static class EnumerableHelper
    {
        /// <summary>
        /// Performs the specified action on each element of the <see cref="IEnumerable&lt;T&gt;"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects to enumerate.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The <see cref="Action&lt;T&gt;"/> delegate to perform on each element of the <see cref="IEnumerable&lt;T&gt;"/>.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}
