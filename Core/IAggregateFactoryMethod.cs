namespace Core
{
    public interface IAggregateFactoryMethod<out T> where T : IAggregate
    {
        T CreateAggregate();
    }
}
