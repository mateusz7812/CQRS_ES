using System;
using Commands;
using Core;
using Events;
using ReadDB;

namespace Applications
{
    public class Application : IApplication
    {
        public Application(ObservableEventPublisher observableEventPublisher,
            BusObserverAdapter<IEvent> busObserverAdapter,
            ICommandHandler<CreateAccountCommand> createAccountCommandHandler,
            ICommandHandler<CreateDepositCommand> createDepositCommandHandler,
            IEventHandler<CreateAccountEvent> createAccountEventHandler,
            IEventHandler<CreateDepositEvent> createDepositEventHandler,
            IEventHandler<AddDepositToAccountEvent> addDepositToAccountEventHandler,
            IHandlerFactoryMethod<IEvent> eventHandlerFactoryMethod,
            IHandlerFactoryMethod<ICommand> commandHandlerFactoryMethod)
        {
            eventHandlerFactoryMethod.AddHandler(createAccountEventHandler);
            eventHandlerFactoryMethod.AddHandler(createDepositEventHandler);
            eventHandlerFactoryMethod.AddHandler(addDepositToAccountEventHandler);

            commandHandlerFactoryMethod.AddHandler(createAccountCommandHandler);
            commandHandlerFactoryMethod.AddHandler(createDepositCommandHandler);

            observableEventPublisher.AddObserver(busObserverAdapter);
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}