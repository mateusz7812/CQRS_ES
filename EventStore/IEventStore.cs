using System;
using System.Collections.Generic;
using System.Text;
using EventsAndCommands;

namespace EventStore
{
    public interface IEventStore
    {
        void Save(IEvent @event);
        List<IEvent> FindByItemGuid(Guid itemGuid);
    }
}
