using System;
using System.Collections.Generic;
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

        private static readonly Dictionary<Type, bool> ReferenceEquatableTypes = new Dictionary<Type, bool>();

        /// <summary>
        /// Returns <c>true</c> if this type uses reference equality (i.e., does not override <see cref="object.Equals(object)"/>);
        /// returns <c>false</c> if this type or any of its base types override <see cref="object.Equals(object)"/>. 
        /// This method returns <c>false</c> for any interface type, and returns <c>true</c> for any reference-equatable base class 
        /// even if a derived class is not reference-equatable;
        /// the best way to determine if an object uses reference equality is to pass the exact type of that object.
        /// </summary>
        /// <param name="type">The type to test for reference equality. May not be <c>null</c>.</param>
        /// <returns>
        /// Returns <c>true</c> if this type uses reference equality (i.e., does not override <see cref="object.Equals(object)"/>); 
        /// returns <c>false</c> if this type or any of its base types override <see cref="object.Equals(object)"/>.
        /// </returns>
        public static bool IsReferenceEquatable(this Type type)
        {
            if (type.IsPointer)
                return false;

            var typeInfo = type.GetTypeInfo();
            if (!typeInfo.IsClass)
                return false;

            lock (ReferenceEquatableTypes)
            {
                bool isReferenceEquatable;
                if (!ReferenceEquatableTypes.TryGetValue(type, out isReferenceEquatable))
                {
                    isReferenceEquatable = OverridesEquals(new FullTypeInfo { Type = type, TypeInfo = typeInfo });
                    ReferenceEquatableTypes[type] = isReferenceEquatable;
                }

                return isReferenceEquatable;
            }
        }

        private static bool OverridesEquals(FullTypeInfo specificType)
        {
            foreach (var type in TypeAndBaseTypesExceptObject(specificType))
            {
                foreach (var method in type.TypeInfo.DeclaredMethods)
                {
                    if (!method.IsPublic || method.IsStatic || !method.IsVirtual || !method.IsHideBySig 
                        || !method.Name.Equals("Equals", StringComparison.Ordinal))
                        continue;

                    var baseDefinition = method.GetRuntimeBaseDefinition();
                    if (baseDefinition == method)
                        continue;

                    if (baseDefinition.DeclaringType == typeof(object))
                        return true;
                }
            }

            return false;
        }

        private static IEnumerable<FullTypeInfo> TypeAndBaseTypesExceptObject(FullTypeInfo type)
        {
            if (type.Type == null || type.Type == typeof(object))
                yield break;

            while (true)
            {
                yield return type;

                type.Type = type.TypeInfo.BaseType;
                if (type.Type == null || type.Type == typeof(object))
                    yield break;

                type.TypeInfo = type.Type.GetTypeInfo();
            }
        }

        private struct FullTypeInfo
        {
            public Type Type { get; set; }
            public TypeInfo TypeInfo { get; set; }
        }
    }
}
