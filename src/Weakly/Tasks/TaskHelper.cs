﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Weakly
{
    /// <summary>
    /// Helper to create completed, canceled and faulted tasks.
    /// </summary>
    public static class TaskHelper
    {
        private readonly static Task CompletedTask = Task.FromResult<object>(null);

        /// <summary>
        /// Gets an already completed task.
        /// </summary>
        /// <returns>The completed task.</returns>
        public static Task Completed()
        {
            return CompletedTask;
        }

        private static class CanceledTask<TResult>
        {
            public static readonly Task<TResult> Task = CreateCanceled();

            private static Task<TResult> CreateCanceled()
            {
                var tcs = new TaskCompletionSource<TResult>();
                tcs.TrySetCanceled();
                return tcs.Task;
            }
        }

        /// <summary>
        /// Gets an already canceled task.
        /// </summary>
        /// <returns>The canceled task.</returns>
        public static Task Canceled()
        {
            return CanceledTask<object>.Task;
        }

        /// <summary>
        /// Gets an already canceled task.
        /// </summary>
        /// <returns>The canceled task.</returns>
        public static Task<TResult> Canceled<TResult>()
        {
            return CanceledTask<TResult>.Task;
        }

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

        /// <summary>
        /// Suppresses default exception handling of a Task that would otherwise reraise the exception on the finalizer thread.
        /// </summary>
        /// <param name="task">The Task to be monitored.</param>
        /// <returns>The original Task.</returns>
        public static Task IgnoreExceptions(this Task task)
        {
            // ReSharper disable once UnusedVariable
            task.ContinueWith(t => { var ignored = t.Exception; },
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted,
                TaskScheduler.Default);
            return task;
        }

        /// <summary>
        /// Suppresses default exception handling of a Task that would otherwise reraise the exception on the finalizer thread.
        /// </summary>
        /// <param name="task">The Task to be monitored.</param>
        /// <returns>The original Task.</returns>
        public static Task<T> IgnoreExceptions<T>(this Task<T> task)
        {
            return (Task<T>)((Task)task).IgnoreExceptions();
        }
    }
}
