﻿using Core;
using Events;
using Models;

namespace AccountModule.CreateAccount
{
    public class CreateAccountEventHandler : IHandler<IEvent>
    {
        private readonly IService<AccountModel> _accountService;

        public CreateAccountEventHandler(IService<AccountModel> accountService)
        {
            _accountService = accountService;
        }

        public void Handle(IEvent item)
        {
            var createAccountEvent = (CreateAccountEvent) item;
            var account = new AccountModel{Guid = createAccountEvent.ItemGuid, Name = createAccountEvent.AccountName};
            _accountService.Save(account);
        }

        public bool CanHandle(IEvent item)
        {
            return item is CreateAccountEvent;
        }
    }
}