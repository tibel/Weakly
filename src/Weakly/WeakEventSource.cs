using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Weakly
{
    public class WeakEventSource<TEventArgs>
        where TEventArgs : EventArgs
    {
        private EventHandler<TEventArgs> _staticEventHandlers;
        private readonly int _invokationsToCompileDelegate;
        private readonly List<EventHandlerEntry> _eventHandlerEntries;
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
                if (!entry.TargetReference.IsAlive)
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

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakEventSource&lt;TEventHandler&gt;"/> class.
        /// </summary>
        /// <param name="invokationsToCompileDelegate">The number of invokations on which the delegate will be compiled.</param>
        public WeakEventSource(int invokationsToCompileDelegate = 4)
        {
            if (invokationsToCompileDelegate <= 0)
                throw new ArgumentOutOfRangeException("invokationsToCompileDelegate", "Value must be greater than zero.");

            _invokationsToCompileDelegate = invokationsToCompileDelegate;
            _eventHandlerEntries = new List<EventHandlerEntry>();
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
                _eventHandlerEntries.Add(new EventHandlerEntry(d.Target, d.Method));
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
                    var target = entry.TargetReference.Target;

                    if (target == d.Target && entry.TargetMethod == d.Method.MethodHandle)
                    {
                        _eventHandlerEntries.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void Raise(object sender, TEventArgs e)
        {
            EventHandlerEntry[] invocationList;
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
                entry.Invoke(_invokationsToCompileDelegate, sender, e);
            }
        }

        #region Inner Types

        private sealed class EventHandlerEntry
        {
            public readonly RuntimeMethodHandle TargetMethod;
            public readonly WeakReference TargetReference;
            private int _callCount;
            private Action<object, object, TEventArgs> _fastCall;

            public EventHandlerEntry(object target, MethodInfo method)
            {
                TargetMethod = method.MethodHandle;
                TargetReference = new WeakReference(target);
            }

            public void Invoke(int invokationsToCompile, object sender, TEventArgs e)
            {
                var target = TargetReference.Target;
                if (target == null)
                    return;

                if (_fastCall == null)
                {
                    _callCount++;
                    if (_callCount >= invokationsToCompile)
                        _fastCall = CreateAction(TargetMethod);
                }

                if (_fastCall != null)
                {
                    _fastCall(target, sender, e);                   
                }
                else
                {
                    var method = (MethodInfo)MethodBase.GetMethodFromHandle(TargetMethod);
                    var parameters = new[] { sender, e };
                    method.Invoke(target, parameters);
                }   
            }

            private static Action<object, object, TEventArgs> CreateAction(RuntimeMethodHandle methodHandle)
            {
                var method = (MethodInfo)MethodBase.GetMethodFromHandle(methodHandle);

                var target = Expression.Parameter(typeof(object), "target");
                var sender = Expression.Parameter(typeof(object), "sender");
                var args = Expression.Parameter(typeof(TEventArgs), "args");

                var convertedTarget = Expression.Convert(target, method.DeclaringType);
                var body = Expression.Call(convertedTarget, method, sender, args);
                return Expression.Lambda<Action<object, object, TEventArgs>>(body, target, sender, args).Compile();
            }
        }

        #endregion
    }
}
