using System;
using CommandBus;
using CommandHandlers;
using CommandHandlers.Aggregates;
using CommandHandlers.AggregatesServices;
using CommandHandlers.CommandHandlers;
using Commands.Commands;
using EventBus;
using EventHandlers;
using EventHandlers.EventHandlers;
using EventHandlers.Repositories;
using EventHandlers.Services;
using EventsAndCommands;
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
            var accountAggregateService = new AggregateService<Account>(eventRepository);
            var createAccountCommandHandler = new CreateAccountCommandHandler(accountAggregateService);
            
            var commandBus = new DefaultCommandBus();
            commandBus.AddCommandHandler(createAccountCommandHandler);

            var eventBus = new DefaultEventBus();
            createAccountCommandHandler.AddObserver(eventBus);

            var accountsRepository = new AccountSqlLiteRepository("accounts");
            accountsRepository.CreateTable();

            var accountService = new AccountService(accountsRepository);
            var createAccountEventHandler = new CreateAccountEventHandler(accountService);
            eventBus.AddEventHandler(createAccountEventHandler);

            var accountId = Guid.NewGuid();
            var accountCreateCommand = new CreateAccountCommand(accountId);

            commandBus.AddCommand(accountCreateCommand);
            commandBus.HandleNext();

            var events = eventRepository.GetEventsOfItemGuid(accountId);
            Assert.Collection(events, @event => Assert.Equal(accountId, @event.ItemGuid));


            eventBus.HandleNext();

            var account = accountService.FindById(accountId);
            Assert.Equal(accountId, account.Guid);
        }
    }
}
