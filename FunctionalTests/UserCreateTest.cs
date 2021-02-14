using Core;
using System;
using AccountModule.CreateAccount;
using AccountModule.Read;
using AccountModule.Write;
using Xunit;

namespace FunctionalTests
{
    public class UserCreateTest
    {
        [Fact]
        public void Test1()
        {
            var accountsRepository = new AccountSqlLiteRepository("accounts");
            accountsRepository.CreateTable();

            var accountService = new AccountService(accountsRepository);
            var createAccountEventHandler = new CreateAccountEventHandler(accountService);

            var eventStore = new DefaultEventStore();
            var eventRepository = new Repository(eventStore);
            var aggregateFactoryMethod = new AccountAggregateFactoryMethod();
            var accountAggregateService = new AggregateService<AccountAggregate>(eventRepository, aggregateFactoryMethod);
            var createAccountCommandHandler = new CreateAccountCommandHandler(accountAggregateService);

            var commandBusCache = new Cache<ICommand>();
            var commandHandlerFactoryMethod = new HandlerFactoryMethod<ICommand>();
            commandHandlerFactoryMethod.AddHandler(createAccountCommandHandler);
            var commandBus = new Bus<ICommand>(commandHandlerFactoryMethod, commandBusCache);

            var eventBusCache = new Cache<IEvent>();
            var eventHandlerFactoryMethod = new HandlerFactoryMethod<IEvent>();
            eventHandlerFactoryMethod.AddHandler(createAccountEventHandler);
            
            var eventBus = new Bus<IEvent>(eventHandlerFactoryMethod, eventBusCache);

            var busObserverAdapter = new BusObserverAdapter<IEvent>(eventBus);
            createAccountCommandHandler.AddObserver(busObserverAdapter);

            var accountId = Guid.NewGuid();
            var accountCreateCommand = new CreateAccountCommand(accountId);

            commandBus.Add(accountCreateCommand);
            commandBus.HandleNext();

            var events = eventRepository.GetByItemGuid(accountId);
            Assert.Collection(events, @event => Assert.Equal(accountId, @event.ItemGuid));

            eventBus.HandleNext();

            var account = accountService.FindById(accountId);
            Assert.Equal(accountId, account.Guid);
        }
    }
}
