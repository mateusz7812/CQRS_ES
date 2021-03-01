using System;
using System.Linq;
using AccountModule.CreateAccount;
using AccountModule.Write;
using Commands;
using Core;
using Currencies;
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
        public void UserCreateTest()
        {
            const string accountName = "TestName";
            var accountCreateCommand = new CreateAccountCommand {Name = accountName};

            _env.HandleCommand(accountCreateCommand);
            
            var allAccounts = _env.AccountModelService.FindAll();
            Assert.Single(allAccounts);
            var account = allAccounts.First();
            Assert.NotEqual(Guid.Empty, account.Guid);
            Assert.Equal(accountName, account.Name);
            Assert.Empty(account.Deposits);
        }

        [Fact]
        public void CreateDepositTest()
        {
            _env.HandleCommand(new CreateAccountCommand { Name = "TestName" });
            var accountId = _env.AccountModelService.FindAll().First().Guid;

            _env.HandleCommand(new CreateDepositCommand { AccountId = accountId });

            var allDeposits = _env.DepositModelService.FindAll();
            Assert.Single(allDeposits);
            var depositModel = allDeposits.First();
            Assert.Equal(accountId, depositModel.Account.Guid);
            var depositId = depositModel.Guid;
            AccountModel accountModel = _env.AccountModelService.FindById(accountId);
            var deposits = accountModel.Deposits;
            Assert.Single(deposits);
            Assert.Equal(depositId, deposits.First().Guid);
        }

        [Fact]
        public void TransferMoneyTest()
        {
            var firstName = "first";
            _env.HandleCommand(new CreateAccountCommand { Name = firstName });
            var firstAccountGuid = _env.AccountModelService.FindAll(model => model.Name == firstName).First().Guid;
            _env.HandleCommand(new CreateDepositCommand { AccountId = firstAccountGuid });

            var secondName = "second";
            _env.HandleCommand(new CreateAccountCommand { Name = secondName });
            var secondAccountGuid = _env.AccountModelService.FindAll(model => model.Name == secondName).First().Guid;
            _env.HandleCommand(new CreateDepositCommand { AccountId = secondAccountGuid });

            var firstDepositId = _env.AccountModelService.FindById(firstAccountGuid).Item.Deposits.First().Guid;
            var dollarToTransferOnDepositByAtm = Currencies.Dollars.Of(100.10);

            _env.HandleCommand(new CreateAtmCommand());
            var atmId = _env.AtmModelService.FindAll().First().Guid;

            _env.HandleCommand(new TransferOnDepositByAtmCommand{AtmId = atmId, DepositId = firstDepositId, Currency = dollarToTransferOnDepositByAtm});
            DepositModel firstDepositToCheckPayByAtm = _env.DepositModelService.FindById(firstDepositId);
            Assert.Equal(dollarToTransferOnDepositByAtm.CurrencyValue, firstDepositToCheckPayByAtm.CurrencyValue);
            Assert.Equal(dollarToTransferOnDepositByAtm.CurrencyType, firstDepositToCheckPayByAtm.CurrencyType);

            var dollarToTransferOnDepositFromDeposit = Currencies.Dollars.Of(20.02);
            var secondDepositId = _env.AccountModelService.FindById(secondAccountGuid).Item.Deposits.First().Guid;
            _env.HandleCommand(new TransferOnDepositFromDepositCommand{SourceDepositId = firstDepositId, DestinationDepositId = secondDepositId, Currency = dollarToTransferOnDepositFromDeposit});

            DepositModel firstDepositToTestPayFromDeposit = _env.DepositModelService.FindById(firstDepositId);
            DepositModel secondDepositToTestPayFromDeposit = _env.DepositModelService.FindById(secondDepositId);

            Assert.Equal(dollarToTransferOnDepositByAtm.CurrencyValue - dollarToTransferOnDepositFromDeposit.CurrencyValue, firstDepositToTestPayFromDeposit.CurrencyValue);
            Assert.Equal(CurrenciesEnum.USD, firstDepositToTestPayFromDeposit.CurrencyType);
            Assert.Equal(dollarToTransferOnDepositFromDeposit.CurrencyValue, secondDepositToTestPayFromDeposit.CurrencyValue);
            Assert.Equal(dollarToTransferOnDepositFromDeposit.CurrencyType, secondDepositToTestPayFromDeposit.CurrencyType);
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
        private AccountModelRepository _accountRepository;
        private DepositModelRepository _depositRepository;
        private EventRepository _eventRepository;

        //Model services
        public Service<AccountModel> AccountModelService;
        public Service<DepositModel> DepositModelService;
        public Service<AtmModel> AtmModelService;

        //Event handlers
        private CreateAccountEventHandler _createAccountEventHandler;
        private CreateDepositEventHandler _createDepositEventHandler;
        private AddDepositToAccountEventHandler _addDepositToAccountEventHandler;

        //Aggregate services
        private AggregateService<AccountAggregate> _accountAggregateService;
        private AggregateService<DepositAggregate> _depositAggregateService;

        //Command handlers
        private CreateAccountCommandHandler _createAccountCommandHandler;
        private CreateDepositCommandHandler _createDepositCommandHandler;

        //Event Publisher
        private readonly ObservableEventPublisher _observableEventPublisher;

        //Buses
        private Bus<ICommand> _commandBus;
        private Bus<IEvent> _eventBus;



        private void SetEventHandlers()
        {
            AccountModelService = new Service<AccountModel>(_accountRepository);
            DepositModelService = new Service<DepositModel>(_depositRepository);

            _createAccountEventHandler = new CreateAccountEventHandler(AccountModelService);
            _createDepositEventHandler = new CreateDepositEventHandler(DepositModelService);
            _addDepositToAccountEventHandler = new AddDepositToAccountEventHandler();
        }

        private void SetRepositories()
        {
            _accountRepository = new AccountModelRepository(_ctxFactoryMethod);
            _depositRepository = new DepositModelRepository(_ctxFactoryMethod);
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
            _commandBus = new Bus<ICommand>(commandHandlerFactoryMethod, commandBusCache);

            var eventBusCache = new Cache<IEvent>();
            var eventHandlerFactoryMethod = new HandlerFactoryMethod<IEvent>();
            eventHandlerFactoryMethod.AddHandler(_createAccountEventHandler);
            eventHandlerFactoryMethod.AddHandler(_createDepositEventHandler);
            eventHandlerFactoryMethod.AddHandler(_addDepositToAccountEventHandler);
            _eventBus = new Bus<IEvent>(eventHandlerFactoryMethod, eventBusCache);

            var busObserverAdapter = new BusObserverAdapter<IEvent>(_eventBus);
            _observableEventPublisher.AddObserver(busObserverAdapter);
        }

        public void HandleCommand(ICommand command)
        {
            _commandBus.Add(command);
            _commandBus.HandleNext();
            while (!_eventBus.IsBusEmpty)
                _eventBus.HandleNext();
        }

        public void Dispose()
        {
            _ctxFactoryMethod.Create().Database.Delete();
        }
    }
}