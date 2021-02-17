using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Principal;
using System.Text;
using AccountModule.Read;
using Models;

namespace ReadDB
{
    class ModelDbContext: DbContext
    {
        public ModelDbContext() : base()
        {

        }

        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<DepositModel> Deposits { get; set; }
        
    }
}
