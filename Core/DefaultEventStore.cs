using System;
using System.Collections.Generic;
using System.Linq;
using Optionals;

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

        public Optional<IEvent> FindByEventGuid(Guid guid)
        {
            var events = (from @event in _events
                where @event.EventGuid.Equals(guid)
                select @event).ToList();
            return events.Count switch
            {
                0 => Codes.NotFound,
                1 => new Optional<IEvent> {Item = events.First(), Code = Codes.Success},
                _ => throw new Exception("Two events with same ids")
            };
        }

        public List<IEvent> AllEvents => _events;
    }
}