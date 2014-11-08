﻿using System;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper methods to register or unregister an event handler using reflection.
    /// </summary>
    [Obsolete]
    public static class DynamicEvent
    {
        /// <summary>
        /// Gets the method that adds an event handler to an event source.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>The method used to add an event handler delegate to the event source.</returns>
        public static Action<object, Delegate> GetAddMethod(EventInfo eventInfo)
        {
            return eventInfo.AddEventHandler;
        }

        /// <summary>
        /// Gets the method that removes an event handler from an event source.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>The method used to remove an event handler delegate from the event source.</returns>
        public static Action<object, Delegate> GetRemoveMethod(EventInfo eventInfo)
        {
            return eventInfo.RemoveEventHandler;
        }
    }
}
