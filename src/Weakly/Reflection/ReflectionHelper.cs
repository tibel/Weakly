using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Weakly
{
    /// <summary>
    /// Some useful helpers for <see cref="System.Reflection"/>.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Determines whether the specified member is compiler generated.
        /// </summary>
        /// <param name="memberInfo">The member to examine.</param>
        /// <returns>True, if the member is compiler generated; otherwise false.</returns>
        public static bool IsCompilerGenerated(this MemberInfo memberInfo)
        {
            return memberInfo.IsDefined(typeof (CompilerGeneratedAttribute));
        }

        /// <summary>
        /// Determines whether the specified method is an async method.
        /// </summary>
        /// <param name="methodInfo">The method to examine.</param>
        /// <returns>True, if the method is an async method; otherwise false.</returns>
        public static bool IsAsync(this MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof (AsyncStateMachineAttribute));
        }

        /// <summary>
        /// Determines whether the specified method is an iterator (using yield keyword).
        /// This will only work for C# when using Roslyn compiler.
        /// </summary>
        /// <param name="methodInfo">The method to examine.</param>
        /// <returns>True, if the method is an iterator; otherwise false.</returns>
        public static bool IsIterator(this MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof (IteratorStateMachineAttribute));
        }

        /// <summary>
        /// Determines wether the specified type is a Windows Runtime Type.
        /// </summary>
        /// <param name="type">The type to examine.</param>
        /// <returns>True, if the type is a Windows Runtime Type; otherwise false.</returns>
        public static bool IsWindowsRuntimeType(this Type type)
        {
            return type.AssemblyQualifiedName.EndsWith("ContentType=WindowsRuntime", StringComparison.Ordinal);
        }
    }
}
