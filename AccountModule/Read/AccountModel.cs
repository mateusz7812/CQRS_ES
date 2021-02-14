using System;
using Core;

namespace AccountModule.Read
{
    public class AccountModel : IModel
    {
        public Guid Guid { get; private set; }

        public AccountModel(Guid guid)
        {
            Guid = guid;
        }

    }
}
