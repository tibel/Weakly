using System;
using System.Threading;
using System.Threading.Tasks;

namespace Weakly.MVVM
{
    /// <summary>
    /// Shared UI context
    /// </summary>
    public static class UIContext
    {
        private static Thread _thread;
        private static TaskScheduler _taskScheduler;

        static UIContext()
        {
            _thread = Thread.CurrentThread;
            _taskScheduler = TaskScheduler.Default;
        }

        /// <summary>
        /// Initializes the <see cref="UIContext"/>.
        /// </summary>
        public static void Initialize()
        {
            if (_thread != null)
                throw new InvalidOperationException("UIContext is already initialized.");

            _thread = Thread.CurrentThread;
            _taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        private static void CheckInitialized()
        {
            if (_thread == null)
                throw new InvalidOperationException("UIContext is not initialized.");
        }

        /// <summary>
        /// Determines whether the calling thread is the thread associated with the UI context. 
        /// </summary>
        /// <returns></returns>
        public static bool CheckAccess()
        {
            CheckInitialized();
            return _thread == Thread.CurrentThread;
        }

        /// <summary>
        /// The <see cref="TaskScheduler"/> associated with the UI context.
        /// </summary>
        public static TaskScheduler TaskScheduler
        {
            get
            {
                CheckInitialized();
                return _taskScheduler;
            }
        }
    }
}
