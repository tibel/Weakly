using System;

namespace Weakly
{
    public abstract class WeakDelegate
    {
        private readonly WeakReference _instance;
        private readonly RuntimeMethodHandle _methodHandle;

        protected WeakDelegate(object target, RuntimeMethodHandle methodHandle)
        {
            _instance = new WeakReference(target);
            _methodHandle = methodHandle;
        }

        public bool IsAlive
        {
            get { return _instance.IsAlive; }
        }

        public object Target
        {
            get { return _instance.Target; }
        }

        public RuntimeMethodHandle MethodHandle
        {
            get { return _methodHandle; }
        }
    }
}
