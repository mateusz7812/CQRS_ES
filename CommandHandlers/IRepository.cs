using System;
using System.Collections.Generic;
using System.Text;
using EventsAndCommands;

namespace CommandHandlers
{
    public interface IRepository
    {
        void Save(IEvent @event);
        List<IEvent> GetEventsOfItemGuid(Guid aggregateGuid);
    }
}
