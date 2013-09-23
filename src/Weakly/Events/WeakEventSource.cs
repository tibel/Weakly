using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Weakly
{
    public class WeakEventSource<TEventArgs>
        where TEventArgs : EventArgs
    {
        private EventHandler<TEventArgs> _staticEventHandlers;
        private readonly List<WeakAction<object, TEventArgs>> _eventHandlerEntries = new List<WeakAction<object, TEventArgs>>();
        private WeakReference _gcSentinel = new WeakReference(new object());

        #region Cleanup handling

        private bool IsCleanupNeeded()
        {
            if (_gcSentinel.Target == null)
            {
                _gcSentinel = new WeakReference(new object());
                return true;
            }

            return false;
        }

        private void CleanAbandonedItems()
        {
            for (var i = _eventHandlerEntries.Count - 1; i >= 0; i--)
            {
                var entry = _eventHandlerEntries[i];
                if (!entry.IsAlive)
                    _eventHandlerEntries.RemoveAt(i);
            }
        }

        private void CleanIfNeeded()
        {
            if (IsCleanupNeeded())
            {
                CleanAbandonedItems();
            }
        }

        #endregion

        public void Add(EventHandler<TEventArgs> eventHandler)
        {
            if (eventHandler == null) return;
            var d = (Delegate)eventHandler;

            var declaringType = d.Method.DeclaringType;
            if (declaringType != null && declaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length != 0)
                throw new ArgumentException("Cannot create weak event to anonymous method with closure.");

            if (d.Target == null)
            {
                _staticEventHandlers += eventHandler;
                return;
            }

            lock (_eventHandlerEntries)
            {
                CleanIfNeeded();
                _eventHandlerEntries.Add(new WeakAction<object, TEventArgs>(d.Target, d.Method));
            }
        }

        public void Remove(EventHandler<TEventArgs> eventHandler)
        {
            if (eventHandler == null) return;
            var d = (Delegate)eventHandler;

            if (d.Target == null)
            {
                _staticEventHandlers -= eventHandler;
                return;
            }

            lock (_eventHandlerEntries)
            {
                CleanIfNeeded();

                for (var i = _eventHandlerEntries.Count - 1; i >= 0; i--)
                {
                    var entry = _eventHandlerEntries[i];
                    var target = entry.Target;

                    if (target == d.Target && entry.MethodHandle == d.Method.MethodHandle)
                    {
                        _eventHandlerEntries.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void Raise(object sender, TEventArgs e)
        {
            WeakAction<object, TEventArgs>[] invocationList;
            lock (_eventHandlerEntries)
            {
                CleanIfNeeded();
                invocationList = _eventHandlerEntries.ToArray();
            }

            var staticHandlers = _staticEventHandlers;
            if (staticHandlers != null)
            {
                staticHandlers(sender, e);
            }

            foreach (var entry in invocationList)
            {
                entry.Invoke(sender, e);
            }
        }
    }
}
