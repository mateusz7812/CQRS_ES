using System;
using Optionals;

namespace Core
{
    public class ObservableEventPublisher : AbstractObservable<IEvent>, IEventPublisher
    {
        private readonly IEventRepository _eventRepository;

        public ObservableEventPublisher(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void Publish(IEvent @event)
        {
            if (@event.EventGuid != Guid.Empty)
                @event = new ErrorEvent(Guid.Empty, @event.ItemGuid, @event, "Event guid is generating by publisher");
            if (@event.ItemGuid == Guid.Empty)
                @event = new ErrorEvent(Guid.Empty, @event.ItemGuid, @event, "Item guid is not set");
            @event.EventGuid = GetGuid();
            _eventRepository.Save(@event);
            NotifyObservers(@event);
        }

        private Guid GetGuid()
        {
            while (true)
            {
                var guid = Guid.NewGuid();

                var optional = _eventRepository.GetByEventGuid(guid);
                switch (optional.Code.CodesNumbers)
                {
                    case CodesNumbers.NotFound:
                        return guid;
                    case CodesNumbers.Success:
                        break;
                    case CodesNumbers.DbError:
                        throw new Exception("Database Error");
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}