#if !NETSTANDARD1_0

using System;
using System.ComponentModel;

namespace Weakly
{
    internal sealed class WeakNotifyPropertyChangingHandler<TSubscriber> :
        WeakEventHandlerBase<INotifyPropertyChanging, TSubscriber, PropertyChangingEventArgs>
        where TSubscriber : class
    {
        public WeakNotifyPropertyChangingHandler(INotifyPropertyChanging source, TSubscriber subscriber,
            [EmptyCapture] Action<TSubscriber, object, PropertyChangingEventArgs> weakHandler)
            : base(source, subscriber, weakHandler)
        {
            source.PropertyChanging += OnEvent;
        }

        protected override void RemoveEventHandler(INotifyPropertyChanging source)
        {
            source.PropertyChanging -= OnEvent;
        }
    }
}

#endif
