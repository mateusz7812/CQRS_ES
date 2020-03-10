using System;
using System.Collections.Generic;
using System.Text;
using EventHandlers.Models;

namespace EventHandlers.Services
{
    public class AccountService: Service<Account>
    {
        public AccountService(IRepository repository) : base(repository)
        {
        }
    }
}
