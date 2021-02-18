using System.Data.Entity;
using Models;

namespace ReadDB
{
    public class ModelDbContext: DbContext
    {
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<DepositModel> Deposits { get; set; }
    }
}
