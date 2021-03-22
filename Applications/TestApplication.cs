using Commands;
using Core;
using Events;
using Models;
using ReadDB;

namespace Applications
{
    public class TestApplication : Application, ITestApplication
    {
        private readonly IDbContextFactoryMethod<ModelDbContext> _dbContextFactoryMethod;
        public IBus<ICommand> CommandBus { get; set; }
        public IBus<IEvent> EventBus { get; set; }
        public IService<AccountModel> AccountModelService { get; set; }
        public IService<DepositModel> DepositModelService { get; set; }
        public IService<AtmModel> AtmModelService { get; set; }

        public TestApplication(IDbContextFactoryMethod<ModelDbContext> dbContextFactoryMethod,
            ObservableEventPublisher observableEventPublisher, BusObserverAdapter<IEvent> busObserverAdapter,
            ICommandHandler<CreateAccountCommand> createAccountCommandHandler,
            ICommandHandler<CreateDepositCommand> createDepositCommandHandler,
            IEventHandler<CreateAccountEvent> createAccountEventHandler,
            IEventHandler<CreateDepositEvent> createDepositEventHandler,
            IEventHandler<AddDepositToAccountEvent> addDepositToAccountEventHandler,
            IHandlerFactoryMethod<IEvent> eventHandlerFactoryMethod,
            IHandlerFactoryMethod<ICommand> commandHandlerFactoryMethod,
            IBus<ICommand> commandBus,
            IBus<IEvent> eventBus,
            IService<AccountModel> accountModelService,
            IService<DepositModel> depositModelService,
            IService<AtmModel> atmModelService
            ) 
            : base(observableEventPublisher, busObserverAdapter, createAccountCommandHandler, createDepositCommandHandler,
            createAccountEventHandler, createDepositEventHandler, addDepositToAccountEventHandler,
            eventHandlerFactoryMethod, commandHandlerFactoryMethod)
        {
            _dbContextFactoryMethod = dbContextFactoryMethod;
            _dbContextFactoryMethod.Create().Database.Delete();

            CommandBus = commandBus;
            EventBus = eventBus;
            AccountModelService = accountModelService;
            DepositModelService = depositModelService;
            AtmModelService = atmModelService;
        }

        public void HandleCommand(ICommand command)
        {
            CommandBus.Add(command);
            CommandBus.HandleNext();
            while (!EventBus.IsBusEmpty)
                EventBus.HandleNext();
        }

        public void Dispose()
        {
            _dbContextFactoryMethod.Create().Database.Delete();
        }
    }
}