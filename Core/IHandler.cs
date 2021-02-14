namespace Core
{
    public interface IHandler<T> where T : IHandleable
    {
        void Handle(T item);
        bool CanHandle(T item);
    }
}
