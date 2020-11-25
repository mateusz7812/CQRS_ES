﻿using System;
using System.Collections.Generic;
using CommandHandlers;
using Events;

namespace EventStore
{
    public class Repository : IRepository
    {
        private readonly IEventStore _eventStore;

        public Repository(IEventStore eventStore)
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
