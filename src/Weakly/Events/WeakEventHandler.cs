using System;

namespace Weakly
{
    public static class WeakEventHandler<TEventArgs> 
        where TEventArgs : EventArgs
    {
        public static IDisposable Register<TEventSource, TEventListener>(
            TEventSource senderObject,
            Action<TEventSource, EventHandler<TEventArgs>> registerEvent,
            Action<TEventSource, EventHandler<TEventArgs>> deregisterEvent,
            TEventListener listeningObject,
            Action<TEventListener, object, TEventArgs> forwarderAction
        )
            where TEventSource : class
            where TEventListener : class
        {
            if (senderObject == null)
                throw new ArgumentNullException("senderObject");
            if (listeningObject == null)
                throw new ArgumentNullException("listeningObject");
            VerifyDelegate(registerEvent, "registerEvent");
            VerifyDelegate(deregisterEvent, "deregisterEvent");
            VerifyDelegate(forwarderAction, "forwarderAction");

            var weh = new WeakEventHandlerImpl<TEventSource, TEventListener>(senderObject, deregisterEvent, listeningObject, forwarderAction);
            registerEvent(senderObject, weh.OnEvent);
            return weh;
        }

        private static void VerifyDelegate(Delegate d, string parameterName)
        {
            if (d == null)
                throw new ArgumentNullException(parameterName);
            if (!d.Method.IsStatic)
                throw new ArgumentException(
                    "Delegates used for WeakEventHandler must not capture any variables (must point to static methods).",
                    parameterName);
        }

        #region Inner Types

        private sealed class WeakEventHandlerImpl<TEventSource, TEventListener> : IDisposable
            where TEventSource : class
            where TEventListener : class
        {
            private readonly WeakReference<TEventSource> _senderReference;
            private readonly Action<TEventSource, EventHandler<TEventArgs>> _deregisterEvent;
            private readonly WeakReference<TEventListener> _listenerReference;
            private readonly Action<TEventListener, object, TEventArgs> _forwarderAction;

            internal WeakEventHandlerImpl(TEventSource senderObject,
                Action<TEventSource, EventHandler<TEventArgs>> deregisterEvent,
                TEventListener listenerObject,
                Action<TEventListener, object, TEventArgs> forwarderAction)
            {
                _senderReference = new WeakReference<TEventSource>(senderObject);
                _deregisterEvent = deregisterEvent;
                _listenerReference = new WeakReference<TEventListener>(listenerObject);
                _forwarderAction = forwarderAction;
            }

            public void Dispose()
            {
                var sender = _senderReference.Target;

                if (sender != null)
                {
                    _deregisterEvent(sender, OnEvent);
                    _senderReference.Target = null;
                }
            }

            internal void OnEvent(object sender, TEventArgs args)
            {
                var target = _listenerReference.Target;
                if (target != null)
                {
                    _forwarderAction(target, sender, args);
                }
                else
                {
                    Dispose();
                }
            }
        }

        #endregion
    }
}
