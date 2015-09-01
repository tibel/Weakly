using System;

namespace Weakly
{
    /// <summary>
    /// Indicates that the delegate parameter should not be an instance method or closure (captures no context).
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class EmptyCaptureAttribute : Attribute
    {
    }
}
