using System;

namespace Core
{
    public interface IAggregateService<out T> : IObservable<IEvent> where T : IAggregate
    {
        T Load(Guid aggregateGuid);
        void SaveAndPublish(IEvent @event);
    }
}
