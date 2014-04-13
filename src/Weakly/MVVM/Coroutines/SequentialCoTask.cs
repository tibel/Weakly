using System;
using System.Collections.Generic;

namespace Weakly.MVVM
{
    /// <summary>
    /// An implementation of <see cref = "ICoTask" /> that enables sequential execution of multiple CoTasks.
    /// </summary>
    internal sealed class SequentialCoTask : CoTask
    {
        private readonly IEnumerator<ICoTask> _enumerator;
        private CoroutineExecutionContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref = "SequentialCoTask" /> class.
        /// </summary>
        /// <param name = "enumerator">The enumerator.</param>
        public SequentialCoTask(IEnumerator<ICoTask> enumerator)
        {
            if (enumerator == null)
                throw new ArgumentNullException("enumerator");

            _enumerator = enumerator;
        }

        /// <summary>
        /// Executes the CoTask using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CoroutineExecutionContext context)
        {
            _context = context;
            ChildCompleted(null, new CoTaskCompletedEventArgs(null, false));
        }

        private void ChildCompleted(object sender, CoTaskCompletedEventArgs args)
        {
            var previous = sender as ICoTask;
            if (previous != null)
            {
                previous.Completed -= ChildCompleted;
            }

            if (args.Error != null || args.WasCancelled)
            {
                OnComplete(args.Error, args.WasCancelled);
                return;
            }

            bool moveNextSucceeded;
            try
            {
                moveNextSucceeded = _enumerator.MoveNext();
            }
            catch (Exception ex)
            {
                OnComplete(ex, false);
                return;
            }

            if (moveNextSucceeded)
            {
                try
                {
                    var next = _enumerator.Current;
                    Coroutine.BuildUp(next);
                    next.Completed += ChildCompleted;
                    next.Execute(_context);
                }
                catch (Exception ex)
                {
                    OnComplete(ex, false);
                }
            }
            else
            {
                OnComplete(null, false);
            }
        }

        private void OnComplete(Exception error, bool wasCancelled)
        {
            _context = null;
            _enumerator.Dispose();
            OnCompleted(new CoTaskCompletedEventArgs(error, wasCancelled));
        }
    }
}
