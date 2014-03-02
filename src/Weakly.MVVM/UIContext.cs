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
        private static bool _isInDesignTool;

        /// <summary>
        /// Initializes the <see cref="UIContext"/>.
        /// </summary>
        /// <param name="isInDesignTool">Whether or not the framework is running in the context of a designer.</param>
        public static void Initialize(bool isInDesignTool)
        {
            if (_thread != null)
                throw new InvalidOperationException("UIContext is already initialized.");

            _thread = Thread.CurrentThread;
            _taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            _isInDesignTool = isInDesignTool;
        }

        private static void VerifyInitialized()
        {
            if (_thread == null)
                throw new InvalidOperationException("UIContext is not initialized.");
        }

        /// <summary>
        /// Indicates whether or not the framework is running in the context of a designer.
        /// </summary>
        public static bool IsInDesignTool
        {
            get
            {
                VerifyInitialized();
                return _isInDesignTool;
            }
        }

        /// <summary>
        /// Determines whether the calling thread is the thread associated with the UI context. 
        /// </summary>
        /// <returns></returns>
        public static bool CheckAccess()
        {
            VerifyInitialized();
            return _thread == Thread.CurrentThread;
        }

        /// <summary>
        /// The <see cref="TaskScheduler"/> associated with the UI context.
        /// </summary>
        public static TaskScheduler TaskScheduler
        {
            get
            {
                VerifyInitialized();
                return _taskScheduler;
            }
        }
    }
}
