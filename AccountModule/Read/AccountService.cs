using Core;

namespace AccountModule.Read
{
    public class AccountService : Service<AccountModel>
    {
        public AccountService(IRepository<AccountModel> repository) : base(repository)
        {
        }
    }
}
