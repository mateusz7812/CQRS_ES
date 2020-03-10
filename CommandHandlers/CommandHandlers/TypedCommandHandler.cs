using System;
using System.Collections.Generic;
using System.Text;
using EventsAndCommands;

namespace CommandHandlers.CommandHandlers
{
    public abstract class TypedCommandHandler<T> : ICommandHandler where T: ICommand
    {
        public Type CommandType => typeof(T);

        public abstract void Handle(ICommand command);
        public abstract bool CommandIsCorrect(ICommand command);
    }
}
