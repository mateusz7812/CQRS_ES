using System;
using System.Collections.Generic;
using Aggregates;
using CommandHandlers;
using CommandHandlers.AggregatesServices;
using CommandHandlers.CommandHandlers;
using Commands.Commands;
using Events;
using Events.Events;

namespace AccountComponents
{
    public class CreateAccountCommandHandler : TypedCommandHandler<CreateAccountCommand>, IObservable
    {
        private readonly IAggregateService<Account> _accountService;
        private readonly IList<IObserver> _observers;

        public CreateAccountCommandHandler(IAggregateService<Account> accountService)
        {
            _accountService = accountService;
            _observers = new List<IObserver>();
        }

        public override void Handle(CreateAccountCommand command)
        {
            var accountGuid = command.AccountGuid;
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), accountGuid);
            _accountService.SaveAndPublish(createAccountEvent);
            NotifyObservers(createAccountEvent);
        }

        public override bool CommandIsCorrect(CreateAccountCommand command) => 
            command.AccountGuid != Guid.Empty;

        public void AddObserver(IObserver observer) => 
            _observers.Add(observer);

        public void NotifyObservers(IEvent @event)
        {
            foreach (var observer in _observers) 
                observer.Update(@event);
        }
    }
}
