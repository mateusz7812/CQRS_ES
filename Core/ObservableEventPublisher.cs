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
            _eventRepository.Save(@event);
            NotifyObservers(@event);
        }
    }
}