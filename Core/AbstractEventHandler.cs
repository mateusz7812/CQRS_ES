namespace Core
{
    public abstract class AbstractEventHandler<T>: IEventHandler<T> where T: IEvent
    {
        public abstract void Handle(IEvent item);

        public bool CanHandle(IEvent item)
            => item is T;
    }
}