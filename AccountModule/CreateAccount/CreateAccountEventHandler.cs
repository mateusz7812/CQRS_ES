using Core;
using Events;
using Models;

namespace AccountModule.CreateAccount
{
    public class CreateAccountEventHandler : AbstractEventHandler<CreateAccountEvent>
    {
        private readonly IService<AccountModel> _accountService;

        public CreateAccountEventHandler(IService<AccountModel> accountService)
        {
            _accountService = accountService;
        }

        public override void Handle(IEvent item)
        {
            var createAccountEvent = (CreateAccountEvent) item;
            var account = new AccountModel{Guid = createAccountEvent.ItemGuid, Name = createAccountEvent.AccountName};
            _accountService.Save(account);
        }
    }
}