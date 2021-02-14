using AccountModule.Read;
using Core;

namespace AccountModule.CreateAccount
{
    public class CreateAccountEventHandler : IHandler<IEvent>
    {
        private readonly Service<AccountModel> _accountService;

        public CreateAccountEventHandler(Service<AccountModel> accountService)
        {
            _accountService = accountService;
        }

        public void Handle(IEvent item)
        {
            var createAccountEvent = (CreateAccountEvent)item;
            var account = new AccountModel(createAccountEvent.ItemGuid);
            _accountService.Save(account);
        }

        public bool CanHandle(IEvent item)
        {
            return item is CreateAccountEvent;
        }
    }
}