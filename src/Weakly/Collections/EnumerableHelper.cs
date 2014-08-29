using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        /// <param name="source">The enumerable.</param>
        /// <param name="action">The <see cref="Action&lt;T&gt;"/> delegate to perform on each element of the <see cref="IEnumerable&lt;T&gt;"/>.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Performs the specified asynchronous action on each element of the <see cref="IEnumerable&lt;T&gt;"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects to enumerate.</typeparam>
        /// <param name="source">The enumerable.</param>
        /// <param name="asyncAction">The <see cref="Func&lt;T, Task&gt;"/> delegate to perform on each element of the <see cref="IEnumerable&lt;T&gt;"/>.</param>
        /// <returns>A task that represents the completion.</returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> asyncAction)
        {
            return Task.WhenAll(source.Select(asyncAction));
        }

        /// <summary>
        /// Projects asynchronous each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by asyncSelector.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="asyncSelector">An asynchronous transform function to apply to each element.</param>
        /// <returns></returns>
        public static Task<TResult[]> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> asyncSelector)
        {
            return Task.WhenAll(source.Select(asyncSelector));
        }
    }
}
