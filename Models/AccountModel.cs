using System;
using System.Collections.Generic;
using Core;
using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountModule.Read
{
    public class AccountModel : IModel
    {
        [Key]
        public int ModelDbId { get; set; }
        [Index(IsUnique = true)]
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DepositModel> Deposits { get; set; }
    }
}
