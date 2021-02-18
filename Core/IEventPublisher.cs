namespace Core
{
    public interface IEventPublisher
    {
        public void Publish(IEvent @event);
    }
}
