using System;
using System.Reflection;

namespace Weakly
{
    internal class WeakEventHelper
    {
        public static void Add<TEventArgs>(object eventSource, EventInfo eventInfo, Action<object, TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            // create weak handler
            var weakAction = new WeakAction<object, TEventArgs>(handler);
            //TODO: support automatic unregister

            // create correct delegate type
            var weakHandler = Delegate.CreateDelegate(
                eventInfo.EventHandlerType,
                weakAction,
                weakAction.GetType().GetMethod("Invoke"));

            // register weak handler
            var addMethod = DynamicEvent.GetAddMethod(eventInfo);
            addMethod(eventSource, weakHandler);
        }
    }
}
