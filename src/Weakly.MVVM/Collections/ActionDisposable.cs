using System;

namespace Weakly.MVVM
{
    internal sealed class ActionDisposable : IDisposable
    {
        private Action _action;

        public ActionDisposable(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            if (_action == null) return;
            _action();
            _action = null;
        }
    }
}
