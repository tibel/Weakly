using System;
using System.Threading.Tasks;

namespace Weakly
{
    /// <summary>
    /// Provides data for the event that is raised when a faulted <see cref="Task"/> is observed.
    /// </summary>
    public sealed class FaultedTaskEventArgs : EventArgs
    {
        private readonly Task _task;

        /// <summary> 
        /// Initializes a new instance of the <see cref="FaultedTaskEventArgs"/> class with the faulted task.
        /// </summary>
        /// <param name="task">The Task that has faulted.</param> 
        public FaultedTaskEventArgs(Task task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            _task = task;
        }

        /// <summary>
        /// The faulted task. 
        /// </summary>
        public Task Task
        {
            get { return _task; }
        }
    }
}
