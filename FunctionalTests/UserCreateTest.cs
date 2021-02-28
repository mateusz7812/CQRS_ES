using System.Linq;
using AccountModule.CreateAccount;
using AccountModule.Write;
using Commands;
using Core;
using Models;
using ReadDB;
using Xunit;

namespace Tests
{
    [Collection("DbTest")]
    public class UserCreateTest
    {
        [Fact]
        public void Test1()
        {
            //EventHandlers
            var ctxFactoryMethod = new SqLiteCtxFactoryMethod();
            ctxFactoryMethod.Create().Database.Delete();

            var accountsRepository = new AccountModelDbRepository(ctxFactoryMethod);
            var accountService = new Service<AccountModel>(accountsRepository);
            var createAccountEventHandler = new CreateAccountEventHandler(accountService);

            //Aggregates
            var eventStore = new DefaultEventStore();
            var eventRepository = new EventRepository(eventStore);
            
            var aggregateFactoryMethod = new AccountAggregateFactoryMethod();
            var accountAggregateService = new AggregateService<AccountAggregate>(eventRepository, aggregateFactoryMethod);

            //Command handlers
            var observableEventPublisher = new ObservableEventPublisher(eventRepository);

            var createAccountCommandHandler = new CreateAccountCommandHandler(accountAggregateService, observableEventPublisher);

            //Buses
            var commandBusCache = new Cache<ICommand>();
            var commandHandlerFactoryMethod = new HandlerFactoryMethod<ICommand>();
            commandHandlerFactoryMethod.AddHandler(createAccountCommandHandler);
            var commandBus = new Bus<ICommand>(commandHandlerFactoryMethod, commandBusCache);

            var eventBusCache = new Cache<IEvent>();
            var eventHandlerFactoryMethod = new HandlerFactoryMethod<IEvent>();
            eventHandlerFactoryMethod.AddHandler(createAccountEventHandler);
            var eventBus = new Bus<IEvent>(eventHandlerFactoryMethod, eventBusCache);

            var busObserverAdapter = new BusObserverAdapter<IEvent>(eventBus);
            observableEventPublisher.AddObserver(busObserverAdapter);

            //Test
            var accountName = "TestName";
            var accountCreateCommand = new CreateAccountCommand{
                Name = accountName};

            commandBus.Add(accountCreateCommand);
            commandBus.HandleNext();

            Assert.Single(eventStore.AllEvents);
            var accountId = eventStore.AllEvents.First().ItemGuid;

            var events = eventRepository.GetByItemGuid(accountId);
            Assert.Collection(events, @event => Assert.Equal(accountId, @event.ItemGuid));

            eventBus.HandleNext();

            AccountModel account = accountService.FindById(accountId);
            Assert.Equal(accountId, account.Guid);
            Assert.Equal(accountName, account.Name);

            ctxFactoryMethod.Create().Database.Delete();
        }
    }
}
