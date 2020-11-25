using System;
using Events;

namespace CommandHandlers.AggregatesServices
{
    public interface IAggregateService<T>: IObservable<IEvent> where T:IAggregate 
    {
        T Load(Guid aggregateGuid);
        void SaveAndPublish(IEvent @event);
    }
}
