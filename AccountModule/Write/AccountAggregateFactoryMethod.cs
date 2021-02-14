using Core;

namespace AccountModule.Write
{
    public class AccountAggregateFactoryMethod : IAggregateFactoryMethod<AccountAggregate>
    {
        public AccountAggregate CreateAggregate()
        {
            return new AccountAggregate();
        }
    }
}
