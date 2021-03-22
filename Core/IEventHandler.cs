namespace Core
{
    public interface IEventHandler<T> : IHandler<IEvent> where T: IEvent
    {
    }
}
