using System;
using System.Linq;
using AccountModule.CreateAccount;
using AccountModule.Write;
using Commands;
using Core;
using DepositModule.CreateDeposit;
using DepositModule.Write;
using Models;
using ReadDB;
using Xunit;

namespace Tests
{
    [Collection("DbTest")]
    public class FunctionalTests: IClassFixture<EnvironmentFixture>
    {
        private readonly EnvironmentFixture _env;

        public FunctionalTests(EnvironmentFixture env)
        {
            _env = env;
        }

        [Fact]
        public void Test()
        {
            var accountName = "TestName";

            _env.CommandBus.Add(new CreateAccountCommand {Name = accountName});
            _env.CommandBus.HandleNext();
            _env.EventBus.HandleNext();

            var allAccounts = _env.AccountsRepository.FindAll();
            Assert.Single(allAccounts);
            var accountId = allAccounts.First().Guid;

            var createDepositCommand = new CreateDepositCommand {AccountId = accountId};
            _env.HandleCommand(createDepositCommand);

            var findByItemGuid = _env.DepositRepository.FindAll();
            Assert.Single(findByItemGuid);
            var depositModel = findByItemGuid.First();

            Assert.Equal(accountId, depositModel.Account.Guid);
            var depositId = depositModel.Guid;

            AccountModel accountModel = _env.AccountsRepository.FindById(accountId);
            var deposits = accountModel.Deposits;
            Assert.Single(deposits);
            Assert.Equal(depositId, deposits.First().Guid);
        }
    }

    public class EnvironmentFixture: IDisposable
    {
        public EnvironmentFixture()
        {
            _ctxFactoryMethod = new SqLiteCtxFactoryMethod();
            _eventStore = new DefaultEventStore();
            _ctxFactoryMethod.Create().Database.Delete();
            SetRepositories();
            SetEventHandlers();
            SetAggregates();
            _observableEventPublisher = new ObservableEventPublisher(_eventRepository);
            SetCommandHandlers();
            SetBuses();
        }

        private readonly SqLiteCtxFactoryMethod _ctxFactoryMethod;
        private readonly DefaultEventStore _eventStore;

        //Repositories
        public AccountModelDbRepository AccountsRepository;
        public DepositModelRepository DepositRepository;
        private EventRepository _eventRepository;

        //Event handlers
        private CreateAccountEventHandler _createAccountEventHandler;
        private CreateDepositEventHandler _createDepositEventHandler;
        private AddDepositToAccountEventHandler _addDepositToAccountEventHandler;

        //Aggregates
        private AggregateService<AccountAggregate> _accountAggregateService;
        private AggregateService<DepositAggregate> _depositAggregateService;

        //Command handlers
        private CreateAccountCommandHandler _createAccountCommandHandler;
        private CreateDepositCommandHandler _createDepositCommandHandler;

        //Event Publisher
        private readonly ObservableEventPublisher _observableEventPublisher;

        //Buses
        public Bus<ICommand> CommandBus;
        public Bus<IEvent> EventBus;



        private void SetEventHandlers()
        {
            var accountService = new Service<AccountModel>(AccountsRepository);
            var depositService = new Service<DepositModel>(DepositRepository);

            _createAccountEventHandler = new CreateAccountEventHandler(accountService);
            _createDepositEventHandler = new CreateDepositEventHandler(depositService);
            _addDepositToAccountEventHandler = new AddDepositToAccountEventHandler();
        }

        private void SetRepositories()
        {
            AccountsRepository = new AccountModelDbRepository(_ctxFactoryMethod);
            DepositRepository = new DepositModelRepository(_ctxFactoryMethod);
            _eventRepository = new EventRepository(_eventStore);
        }

        private void SetAggregates()
        {
            var accountAggregateFactoryMethod = new AccountAggregateFactoryMethod();
            _accountAggregateService =
                new AggregateService<AccountAggregate>(_eventRepository, accountAggregateFactoryMethod);

            var depositAggregateFactoryMethod = new DepositAggregateFactoryMethod();
            _depositAggregateService =
                new AggregateService<DepositAggregate>(_eventRepository, depositAggregateFactoryMethod);
        }

        private void SetCommandHandlers()
        {
            _createAccountCommandHandler =
                new CreateAccountCommandHandler(_accountAggregateService, _observableEventPublisher);

            _createDepositCommandHandler = new CreateDepositCommandHandler(_depositAggregateService,
                _accountAggregateService, _observableEventPublisher);
        }

        private void SetBuses()
        {
            var commandBusCache = new Cache<ICommand>();
            var commandHandlerFactoryMethod = new HandlerFactoryMethod<ICommand>();
            commandHandlerFactoryMethod.AddHandler(_createAccountCommandHandler);
            commandHandlerFactoryMethod.AddHandler(_createDepositCommandHandler);
            CommandBus = new Bus<ICommand>(commandHandlerFactoryMethod, commandBusCache);

            var eventBusCache = new Cache<IEvent>();
            var eventHandlerFactoryMethod = new HandlerFactoryMethod<IEvent>();
            eventHandlerFactoryMethod.AddHandler(_createAccountEventHandler);
            eventHandlerFactoryMethod.AddHandler(_createDepositEventHandler);
            eventHandlerFactoryMethod.AddHandler(_addDepositToAccountEventHandler);
            EventBus = new Bus<IEvent>(eventHandlerFactoryMethod, eventBusCache);

            var busObserverAdapter = new BusObserverAdapter<IEvent>(EventBus);
            _observableEventPublisher.AddObserver(busObserverAdapter);
        }

        public void HandleCommand(CreateDepositCommand createDepositCommand)
        {
            CommandBus.Add(createDepositCommand);
            CommandBus.HandleNext();
            while (!EventBus.IsBusEmpty)
                EventBus.HandleNext();
        }

        public void Dispose()
        {
            _ctxFactoryMethod.Create().Database.Delete();
        }
    }
}