namespace Core
{
    public abstract class TypedCommandHandler<T> : AbstractObservable<T>, IHandler<ICommand>
        where T : IEvent
    {
        public abstract void Handle(ICommand command);
        public abstract bool CanHandle(ICommand command);
    }
}
