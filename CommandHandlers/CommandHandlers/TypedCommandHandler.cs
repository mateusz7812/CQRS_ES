using System;
using System.Collections.Generic;
using System.Text;
using Bus;
using Commands;
using Events;

namespace CommandHandlers.CommandHandlers
{
    public abstract class TypedCommandHandler<T> : AbstractObservable<T>, IHandler<ICommand>
        where T: IEvent
    {
        public abstract void Handle(ICommand command);
        public abstract bool CanHandle(ICommand command);
    }
}
