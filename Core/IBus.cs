namespace Core
{
    public interface IBus<T>
    {
        void Add(T item);
        void HandleNext();
        bool IsBusEmpty { get; }
    }
}
