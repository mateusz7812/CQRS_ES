using Core;

namespace DepositModule.Write
{
    public class DepositAggregateFactoryMethod: IAggregateFactoryMethod<DepositAggregate>
    {
        public DepositAggregate CreateAggregate() => new();
    }
}