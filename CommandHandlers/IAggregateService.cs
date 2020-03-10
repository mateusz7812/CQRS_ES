using System;
using System.Collections.Generic;
using System.Text;
using EventsAndCommands;

namespace CommandHandlers
{
    public interface IAggregateService<out T> where T:IAggregate 
    {
        T Load(Guid aggregateGuid);
        void SaveAndPublish(IEvent @event);
    }
}
