using System;
using System.Collections.Generic;

namespace Core
{
    public interface IEventStore
    {
        void Save(IEvent @event);
        List<IEvent> FindByItemGuid(Guid itemGuid);
    }
}
