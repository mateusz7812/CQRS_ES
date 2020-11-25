namespace CommandHandlers.AggregateFactoryMethod
{
    public interface IAggregateFactoryMethod
    { 
        IAggregate CreateAggregate<T>();
    }
}
