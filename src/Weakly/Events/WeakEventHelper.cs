using System;
using System.Linq.Expressions;
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
                typeof (WeakAction<object, TEventArgs>).GetMethod("Invoke"));

            // register weak handler
            var addMethod = GetEventMethod(eventInfo.GetAddMethod(true));
            addMethod(eventSource, weakHandler);
        }

        private static readonly GenericMethodCache<Action<object, Delegate>> Cache = new GenericMethodCache<Action<object, Delegate>>(); 

        private static Action<object, Delegate> GetEventMethod(MethodInfo method)
        {
            var action = Cache.GetValueOrNull(method.MethodHandle);
            if (action != null) return action;
            action = CompileEventMethod(method);
            Cache.AddOrReplace(method.MethodHandle, action);
            return action;
        }

        private static Action<object, Delegate> CompileEventMethod(MethodInfo method)
        {
            var target = Expression.Parameter(typeof(object), "target");
            var handler = Expression.Parameter(typeof(Delegate), "handler");

            Expression typedTarget = null;
            if (!method.IsStatic)
            {
                typedTarget = Expression.Convert(target, method.DeclaringType);
            }

            var typedHandler = Expression.Convert(handler, method.GetParameters()[0].ParameterType);
            var body = Expression.Call(typedTarget, method, typedHandler);
            return Expression.Lambda<Action<object, Delegate>>(body, target, handler).Compile();
        }
    }
}
