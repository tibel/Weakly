using System;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Helper to determine the managed runtime.
    /// </summary>
    public static class ManagedRuntime
    {
        #region ManagedRuntimeType

        private static readonly Lazy<ManagedRuntimeType> RuntimeTypeProperty = new Lazy<ManagedRuntimeType>(ResolveRuntimeType);

        /// <summary>
        /// Gets the managed runtime type.
        /// </summary>
        public static ManagedRuntimeType RuntimeType
        {
            get { return RuntimeTypeProperty.Value; }
        }

        private static ManagedRuntimeType ResolveRuntimeType()
        {
            var isMono = Type.GetType("Mono.Runtime") != null;
            if (isMono)
                return ManagedRuntimeType.Mono;

            var isSilverlight = Type.GetType("System.ComponentModel.DesignerProperties, System.Windows, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e") != null;
            if (isSilverlight)
                return ManagedRuntimeType.Silverlight;

            var isNET = Type.GetType("System.ComponentModel.DesignerProperties, PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35") != null;
            if (isNET)
                return ManagedRuntimeType.NET;

            var isWinRT = Type.GetType("Windows.ApplicationModel.DesignMode, Windows, ContentType=WindowsRuntime") != null;
            if (isWinRT)
                return ManagedRuntimeType.WinRT;

            return ManagedRuntimeType.Unknown;
        }

        #endregion

        #region IsPrivateReflectionSupported

        private static readonly Lazy<bool> IsPrivateReflectionSupportedProperty = new Lazy<bool>(ResolveIsPrivateReflectionSupported);

        /// <summary>
        /// Gets a value indicating whether private reflection is supported.
        /// </summary>
        public static bool IsPrivateReflectionSupported
        {
            get { return IsPrivateReflectionSupportedProperty.Value; }
        }

        private static bool ResolveIsPrivateReflectionSupported()
        {
            var inst = new ReflectionDetectionClass();

            try
            {
                var method = typeof(ReflectionDetectionClass).GetTypeInfo().GetDeclaredMethod("Method");
                method.Invoke(inst, null);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private class ReflectionDetectionClass
        {
            // ReSharper disable once UnusedMember.Local
            private void Method()
            {
            }
        }

        #endregion
    }
}
