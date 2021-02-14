namespace Core
{
    public interface ICache<T>
    {
        void Add(T item);
        T First();
        void Remove(T item);
        bool IsEmpty { get; }
    }
}
