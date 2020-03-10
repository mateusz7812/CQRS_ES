using EventHandlers.Models;
using EventHandlers.Services;
using EventsAndCommands;
using EventsAndCommands.Events;

namespace EventHandlers.EventHandlers
{
    public class CreateAccountEventHandler: TypedEventHandler<CreateAccountEvent>
    {
        private readonly AccountService _accountService;

        public CreateAccountEventHandler(AccountService accountService)
        {
            _accountService = accountService;
        }

        public override void Handle(IEvent @event)
        {
            var account = new Account(@event.ItemGuid);
            _accountService.Save(account);
        }
    }
}