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
        {
            if (eventSource == null)
                throw new ArgumentNullException("eventSource");
            if (string.IsNullOrEmpty(eventName))
                throw new ArgumentNullException("eventName");

            var eventInfo = eventSource.GetType().GetRuntimeEvent(eventName);
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
        {
            if (sourceType == null)
                throw new ArgumentNullException("sourceType");
            if (string.IsNullOrEmpty(eventName))
                throw new ArgumentNullException("eventName");

            var eventInfo = sourceType.GetRuntimeEvent(eventName);
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
        {
            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");
            if (handler == null)
                throw new ArgumentNullException("handler");
            if (handler.Target == null)
                throw new ArgumentException("Handler delegate must point to instance method.", "handler");

            if (eventInfo.EventHandlerType.IsWindowsRuntimeType())
                throw new ArgumentException("Windows Runtime events are not supported.", "eventInfo");

            var isStatic = eventInfo.AddMethod.IsStatic;
            if (!isStatic && eventSource == null)
                throw new ArgumentNullException("eventSource");
            if (isStatic && eventSource != null)
                throw new ArgumentException("Event source for static event has to be null.", "eventSource");

            return new WeakEventHandlerImpl<TEventArgs>(eventSource, eventInfo, handler);
        }

        #region Inner Types

        private sealed class WeakEventHandlerImpl<TEventArgs> : IDisposable
        {
            private readonly WeakReference _source;
            private readonly EventInfo _eventInfo;
            private readonly WeakAction<object, TEventArgs> _handler;
            private readonly Delegate _eventHandler;
            private bool _disposed;

            public WeakEventHandlerImpl(object eventSource, EventInfo eventInfo, Action<object, TEventArgs> handler)
            {
                if (eventSource != null)
                    _source = new WeakReference(eventSource);

                _eventInfo = eventInfo;
                _handler = new WeakAction<object, TEventArgs>(handler);
                
                // create correct delegate type
                _eventHandler = new Action<object, TEventArgs>(Invoke)
                    .GetMethodInfo()
                    .CreateDelegate(eventInfo.EventHandlerType, this);

                // register weak handler
                _eventInfo.AddEventHandler(eventSource, _eventHandler);
            }

            private void Invoke(object sender, TEventArgs args)
            {
                if (_handler.IsAlive)
                    _handler.Invoke(sender, args);
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

                _eventInfo.RemoveEventHandler(eventSource, _eventHandler);
                _disposed = true;
            }
        }

        #endregion
    }
}
