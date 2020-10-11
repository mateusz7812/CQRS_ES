using System;

namespace EventHandlers.Models
{
    public class Account: IModel, GraphQl.
    {
        public Guid Guid { get; private set; }

        public Account(Guid guid)
        {
            Guid = guid;
        }

    }
}
