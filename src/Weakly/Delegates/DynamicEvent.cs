using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper methods to register or unregister an event handler using reflection.
    /// </summary>
    public static class DynamicEvent
    {
        /// <summary>
        /// Gets the method that adds an event handler to an event source.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>The method used to add an event handler delegate to the event source.</returns>
        public static Action<object, Delegate> GetAddMethod(EventInfo eventInfo)
        {
            return GetEventMethod(eventInfo.GetAddMethod(true));
        }

        /// <summary>
        /// Gets the method that removes an event handler from an event source.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>The method used to remove an event handler delegate from the event source.</returns>
        public static Action<object, Delegate> GetRemoveMethod(EventInfo eventInfo)
        {
            return GetEventMethod(eventInfo.GetRemoveMethod(true));
        }

        private static readonly GenericMethodCache<Action<object, Delegate>> Cache = new GenericMethodCache<Action<object, Delegate>>();

        private static Action<object, Delegate> GetEventMethod(MethodInfo method)
        {
            var action = Cache.GetValueOrNull(method);
            if (action != null) return action;
            action = CompileEventMethod(method);
            Cache.AddOrReplace(method, action);
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
