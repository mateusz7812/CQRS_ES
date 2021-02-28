using System;
using System.Collections.Generic;
using Optionals;

namespace Core
{
    public interface IEventStore
    {
        void Save(IEvent @event);
        List<IEvent> FindByItemGuid(Guid itemGuid);
        Optional<IEvent> FindByEventGuid(Guid guid);
    }
}
