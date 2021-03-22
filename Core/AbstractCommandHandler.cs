namespace Core
{
    public abstract class AbstractCommandHandler<T>: ICommandHandler<T> where T:ICommand
    {
        protected readonly IEventPublisher _eventPublisher;

        protected AbstractCommandHandler(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public abstract void Handle(ICommand item);

        public bool CanHandle(ICommand item) 
            => item is T;
    }
}