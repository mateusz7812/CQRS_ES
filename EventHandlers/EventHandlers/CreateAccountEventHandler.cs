using Bus;
using EventHandlers.Models;
using EventHandlers.Services;
using Events;
using Events.Events;

namespace EventHandlers.EventHandlers
{
    public class CreateAccountEventHandler: IHandler<IEvent>
    {
        private readonly Service<Account> _accountService;

        public CreateAccountEventHandler(Service<Account> accountService)
        {
            _accountService = accountService;
        }

        public void Handle(IEvent item)
        {
            var createAccountEvent = (CreateAccountEvent) item;
            var account = new Account(createAccountEvent.ItemGuid);
            _accountService.Save(account);
        }

        public bool CanHandle(IEvent item)
        {
            return item is CreateAccountEvent;
        }
    }
}