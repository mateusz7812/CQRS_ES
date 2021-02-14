namespace Core
{
    public interface IHandlerFactoryMethod<T> where T : IHandleable
    {
        IHandler<T> CreateHandler(T handleable);
    }
}
