using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Weakly.MVVM
{
    /// <summary>
    /// Enables loosely-coupled publication of and subscription to events.
    /// </summary>
    public sealed class EventAggregator : IEventAggregator
    {
        private readonly List<Handler> _handlers = new List<Handler>(); 

        /// <summary>
        /// Subscribes the specified handler for messages of type <typeparamref name="TMessage" />.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="handler">The message handler to register.</param>
        /// <param name="threadOption">Specifies on which <see cref="System.Threading.Thread" /> the <paramref name="handler" /> is executed.</param>
        public void Subscribe<TMessage>(Action<TMessage> handler, ThreadOption threadOption = ThreadOption.PublisherThread)
        {
            SubscribeInternal<TMessage>(handler, threadOption);
        }

        /// <summary>
        /// Subscribes the specified handler for messages of type <typeparamref name="TMessage" />.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="handler">The message handler to register.</param>
        /// <param name="threadOption">Specifies on which <see cref="System.Threading.Thread" /> the <paramref name="handler" /> is executed.</param>
        public void Subscribe<TMessage, TResult>(Func<TMessage, TResult> handler, ThreadOption threadOption = ThreadOption.PublisherThread)
        {
            SubscribeInternal<TMessage>(handler, threadOption);
        }

        private void SubscribeInternal<TMessage>(Delegate handler, ThreadOption threadOption)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            var declaringType = handler.Method.DeclaringType;
            if (handler.Target != null && declaringType != null && declaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length != 0)
                throw new ArgumentException("A closure cannot be used to subscribe.", "handler");

            lock (_handlers)
            {
                _handlers.RemoveAll(h => h.IsDead);
                _handlers.Add(new Handler(typeof(TMessage), handler.Target, handler.Method, threadOption));
            }
        }

        /// <summary>
        /// Unsubscribes the specified handler.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="handler">The handler to unsubscribe.</param>
        public void Unsubscribe<TMessage>(Action<TMessage> handler)
        {
            UnsubscribeInternal<TMessage>(handler);
        }

        /// <summary>
        /// Unsubscribes the specified handler.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="handler">The handler to unsubscribe.</param>
        public void Unsubscribe<TMessage, TResult>(Func<TMessage, TResult> handler)
        {
            UnsubscribeInternal<TMessage>(handler);
        }

        private void UnsubscribeInternal<TMessage>(Delegate handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            lock (_handlers)
            {
                _handlers.RemoveAll(
                    h => h.IsDead || (h.MessageType == typeof(TMessage) && h.Target == handler.Target && h.Method == handler.Method));
            }
        }

        /// <summary>
        /// Publishes a message.
        /// </summary>
        /// <param name="message">The message instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Publish(object message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            List<Handler> selectedHandlers;
            lock (_handlers)
            {
                _handlers.RemoveAll(h => h.IsDead);
                var messageType = message.GetType();
                selectedHandlers = _handlers.Where(h => h.MessageType.IsAssignableFrom(messageType)).ToList();
            }

            var tasks = selectedHandlers.Select(h => h.Invoke(message)).ToArray();
            Task.WaitAll(tasks);
        }

        private sealed class Handler
        {
            private readonly Type _messageType;
            private readonly WeakReference _reference;
            private readonly MethodInfo _method;
            private readonly ThreadOption _threadOption;

            public Handler(Type messageType, object target, MethodInfo method, ThreadOption threadOption)
            {
                _messageType = messageType;
                _method = method;
                _threadOption = threadOption;

                if (target != null)
                {
                    _reference = new WeakReference(target);
                }
            }

            public Type MessageType
            {
                get { return _messageType; }
            }

            public object Target
            {
                get { return (_reference != null) ? _reference.Target : null; }
            }

            public MethodInfo Method
            {
                get { return _method; }
            }

            public bool IsDead
            {
                get { return _reference != null && _reference.Target == null; }
            }

            public Task Invoke(object message)
            {
                object target = null;
                if (_reference != null)
                {
                    target = _reference.Target;
                    if (target == null)
                        return TaskHelper.Canceled;
                }

                if (_threadOption == ThreadOption.BackgroundThread)
                {
                    return InvokeWithTaskScheduler(target, _method, message, TaskScheduler.Default);
                }
                
                if (_threadOption == ThreadOption.PublisherThread ||
                    _threadOption == ThreadOption.UIThread && UIContext.CheckAccess())
                {
                    return InvokeOnCurrentThread(target, _method, message);
                }

                return InvokeWithTaskScheduler(target, _method, message, UIContext.TaskScheduler);
            }

            private static Task InvokeWithTaskScheduler(object target, MethodInfo method, object message,
                TaskScheduler taskScheduler)
            {
                return Task.Factory.StartNew(() => DynamicDelegate.From(method).Invoke(target, new[] {message}),
                    CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            }

            private static Task InvokeOnCurrentThread(object target, MethodInfo method, object message)
            {
                try
                {
                    var result = DynamicDelegate.From(method).Invoke(target, new[] {message});
                    return TaskHelper.FromResult(result);
                }
                catch (Exception ex)
                {
                    return TaskHelper.Faulted(ex);
                }
            }
        }
    }
}
