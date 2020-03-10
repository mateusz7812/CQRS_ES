using System;

namespace EventHandlers.Models
{
    public class Account: IModel
    {
        public Guid Guid { get; private set; }

        public Account(Guid guid)
        {
            Guid = guid;
        }

    }
}
