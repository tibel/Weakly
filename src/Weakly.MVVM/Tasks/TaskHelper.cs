using System;
using System.Threading.Tasks;

namespace Weakly.MVVM
{
    /// <summary>
    /// Helper to create completed, canceled and faulted tasks.
    /// </summary>
    public static class TaskHelper
    {
        /// <summary>
        /// Creates a <see cref="Task&lt;TResult&gt;"/> that's completed successfully with the specified result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="result">The result to store into the completed task.</param>
        /// <returns>The successfully completed task.</returns>
        [Obsolete]
        public static Task<TResult> FromResult<TResult>(TResult result)
        {
            var tcs = new TaskCompletionSource<TResult>();
            tcs.TrySetResult(result);
            return tcs.Task;
        }

        /// <summary>
        /// An already completed task.
        /// </summary>
        public readonly static Task Completed = Task.FromResult<object>(null);

        private static Task<TResult> CreateCanceled<TResult>()
        {
            var tcs = new TaskCompletionSource<TResult>();
            tcs.TrySetCanceled();
            return tcs.Task;
        }

        /// <summary>
        /// An already canceled task.
        /// </summary>
        public static readonly Task Canceled = CreateCanceled<object>();

        /// <summary>
        /// Creates a task that is fauled with the specified exception.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="ex">The exception.</param>
        /// <returns>The faulted task.</returns>
        public static Task<TResult> Faulted<TResult>(Exception ex)
        {
            var tcs = new TaskCompletionSource<TResult>();
            tcs.TrySetException(ex);
            return tcs.Task;
        }

        /// <summary>
        /// Creates a task that is fauled with the specified exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>The faulted task.</returns>
        public static Task Faulted(Exception ex)
        {
            return Faulted<object>(ex);
        }
    }
}
