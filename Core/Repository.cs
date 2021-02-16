using System;
using System.Collections.Generic;

namespace Core
{
    public class EventRepository : IEventRepository
    {
        private readonly IEventStore _eventStore;

        public EventRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public List<IEvent> GetByItemGuid(Guid itemGuid)
        {
            return _eventStore.FindByItemGuid(itemGuid);
        }

        public void Save(IEvent @event)
        {
            _eventStore.Save(@event);
        }
    }
}
