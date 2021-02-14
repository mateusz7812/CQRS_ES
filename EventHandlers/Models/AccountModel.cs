using System;

namespace EventHandlers.Models
{
    public class AccountModule: IModel
    {
        public Guid Guid { get; private set; }

        public AccountModule(Guid guid)
        {
            Guid = guid;
        }

    }
}
