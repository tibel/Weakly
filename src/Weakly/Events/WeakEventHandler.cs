using System;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// A weak event handler using reflection to register and unregister.
    /// </summary>
    public static class WeakEventHandler
    {
        /// <summary>
        /// Registers for the specified event without holding a strong reference to the <paramref name="handler"/>.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="eventSource">The event source.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="handler">The handler to register.</param>
        /// <returns>A registration object that can be used to deregister from the event.</returns>
        public static IDisposable Register<TEventArgs>(object eventSource, string eventName, Action<object, TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            if (eventSource == null)
                throw new ArgumentNullException("eventSource");
            if (string.IsNullOrEmpty(eventName))
                throw new ArgumentNullException("eventName");

            var eventInfo = eventSource.GetType().GetEvent(eventName);
            return Register(eventSource, eventInfo, handler);
        }

        /// <summary>
        /// Registers for the specified static event without holding a strong reference to the <paramref name="handler"/>.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="sourceType">The event source type.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="handler">The handler to register.</param>
        /// <returns>A registration object that can be used to deregister from the event.</returns>
        public static IDisposable Register<TEventArgs>(Type sourceType, string eventName, Action<object, TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            if (sourceType == null)
                throw new ArgumentNullException("sourceType");
            if (string.IsNullOrEmpty(eventName))
                throw new ArgumentNullException("eventName");

            var eventInfo = sourceType.GetEvent(eventName);
            return Register(null, eventInfo, handler);
        }

        /// <summary>
        /// Registers for the specified event without holding a strong reference to the <paramref name="handler"/>.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="eventSource">The event source.</param>
        /// <param name="eventInfo">The event information.</param>
        /// <param name="handler">The handler to register.</param>
        /// <returns>A registration object that can be used to deregister from the event.</returns>
        public static IDisposable Register<TEventArgs>(object eventSource, EventInfo eventInfo, Action<object, TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");
            if (handler == null)
                throw new ArgumentNullException("handler");
            if (handler.Target == null)
                throw new ArgumentException("Handler delegate must point to instance method.", "handler");

            var isStatic = eventInfo.GetAddMethod(true).IsStatic;
            if (!isStatic && eventSource == null)
                throw new ArgumentNullException("eventSource");
            if (isStatic && eventSource != null)
                throw new ArgumentException("Event source for static event has to be null.", "eventSource");

            return new WeakEventHandlerImpl<TEventArgs>(eventSource, eventInfo, handler);
        }

        #region Inner Types

        private sealed class WeakEventHandlerImpl<TEventArgs> : IDisposable
            where TEventArgs : EventArgs
        {
            private readonly WeakReference _source;
            private readonly WeakReference _target;
            private readonly MethodInfo _handler;
            private readonly EventInfo _eventInfo;
            private readonly Delegate _eventHandler;
            private bool _disposed;

            public WeakEventHandlerImpl(object eventSource, EventInfo eventInfo, Action<object, TEventArgs> handler)
            {
                if (eventSource != null)
                    _source = new WeakReference(eventSource);

                _eventInfo = eventInfo;
                _target = new WeakReference(handler.Target);
                _handler = handler.Method;

                // create correct delegate type
                _eventHandler = Delegate.CreateDelegate(
                    eventInfo.EventHandlerType,
                    this,
                    new Action<object, TEventArgs>(Invoke).Method);

                // register weak handler
                var addMethod = DynamicEvent.GetAddMethod(eventInfo);
                addMethod(eventSource, _eventHandler);
            }

            public void Invoke(object sender, TEventArgs args)
            {
                var target = _target.Target;
                if (target != null)
                    OpenAction.From<object, TEventArgs>(_handler)(target, sender, args);
                else
                    Dispose();
            }

            public void Dispose()
            {
                if (_disposed)
                    return;

                var isStatic = (_source == null);
                var eventSource = !isStatic ? _source.Target : null;
                if (!isStatic && eventSource == null)
                    return;

                var removeMethod = DynamicEvent.GetRemoveMethod(_eventInfo);
                removeMethod(eventSource, _eventHandler);
                _disposed = true;
            }
        }

        #endregion
    }
}
