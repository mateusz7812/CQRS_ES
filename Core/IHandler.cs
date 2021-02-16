namespace Core
{
    public interface IHandler<in T> where T : IHandleable
    {
        void Handle(T item);
        bool CanHandle(T item);
    }
}
