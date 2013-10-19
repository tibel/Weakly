using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper to register a weak handler to an event.
    /// </summary>
    internal class WeakEventHelper
    {
        /// <summary>
        /// TODO: internal tests only
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="eventInfo">The event information.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="handler">The handler.</param>
        public static void Add<TEventArgs>(EventInfo eventInfo, object sender, EventHandler<TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            // make weak handler and convert to right delegate type
            var weakAction = new WeakAction<object, TEventArgs>(handler.Target, handler.Method);
            var weakHandler = Delegate.CreateDelegate(
                eventInfo.EventHandlerType,
                weakAction,
                typeof (WeakAction<object, TEventArgs>).GetMethod("Invoke"));

            var addMethod = CompileEventMethod(eventInfo.GetAddMethod(true));
            addMethod(sender, weakHandler);
        }

        private static Action<object, Delegate> CompileEventMethod(MethodInfo method)
        {
            var target = Expression.Parameter(typeof(object), "target");
            var handler = Expression.Parameter(typeof(Delegate), "handler");
            var typedTarget = Expression.Convert(target, method.DeclaringType);
            var typedHandler = Expression.Convert(handler, method.GetParameters()[0].ParameterType);
            var body = Expression.Call(typedTarget, method, typedHandler);
            return Expression.Lambda<Action<object, Delegate>>(body, target, handler).Compile();
        }
    }
}
