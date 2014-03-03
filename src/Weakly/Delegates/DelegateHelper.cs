using System;
using System.Runtime.CompilerServices;

namespace Weakly
{
    /// <summary>
    /// Some useful helpers for <see cref="Delegate"/>.
    /// </summary>
    public static class DelegateHelper
    {
        /// <summary>
        /// Determines whether the specified handler is closure.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>Wether the <paramref name="handler"/> is a closure.</returns>
        public static bool IsClosure(this Delegate handler)
        {
            var declaringType = handler.Method.DeclaringType;
            return (handler.Target != null && declaringType != null &&
                    declaringType.GetCustomAttributes(typeof (CompilerGeneratedAttribute), false).Length != 0);
        }
    }
}
