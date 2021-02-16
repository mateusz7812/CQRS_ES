using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class DefaultEventStore : IEventStore
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        public void Save(IEvent @event)
            => _events.Add(@event);

        public List<IEvent> FindByItemGuid(Guid itemGuid)
            => (
                    from @event in _events
                    where @event.ItemGuid.Equals(itemGuid)
                    select @event
                ).ToList();
        public List<IEvent> GetAll => _events;
    }
}
