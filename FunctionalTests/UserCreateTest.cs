using System;
using Bus;
using Cache;
using CommandHandlers.AccountComponents;
using CommandHandlers.AggregateFactoryMethod;
using CommandHandlers.AggregatesServices;
using Commands;
using Commands.Commands;
using EventHandlers.EventHandlers;
using EventHandlers.Repositories;
using EventHandlers.Services;
using Events;
using EventStore;
using Xunit;

namespace FunctionalTests
{
    public class UserCreateTest
    {
        [Fact]
        public void Test1()
        {
            var eventStore = new DefaultEventStore();
            var eventRepository = new Repository(eventStore); 
            var aggregateFactoryMethod = new AggregateFactoryMethod();
            var accountAggregateService = new AggregateService<Account>(eventRepository, aggregateFactoryMethod);
            var createAccountCommandHandler = new CreateAccountCommandHandler(accountAggregateService);

            var cache = new Cache<ICommand>();
            var commandHandlerFactoryMethod = new HandlerFactoryMethod<ICommand>();
            commandHandlerFactoryMethod.AddHandler(createAccountCommandHandler);
            var commandBus = new Bus<ICommand>(commandHandlerFactoryMethod, cache);

            var defaultCache = new Cache<IEvent>();
            var eventHandlerFactoryMethod = new HandlerFactoryMethod<IEvent>();
            var eventBus = new Bus<IEvent>(eventHandlerFactoryMethod, defaultCache);

            var busObserverAdapter = new BusObserverAdapter<IEvent>(eventBus);
            createAccountCommandHandler.AddObserver(busObserverAdapter);

            var accountsRepository = new AccountSqlLiteRepository("accounts");
            accountsRepository.CreateTable();

            var accountService = new AccountService(accountsRepository);
            var createAccountEventHandler = new CreateAccountEventHandler(accountService);
            eventHandlerFactoryMethod.AddHandler(createAccountEventHandler);

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
