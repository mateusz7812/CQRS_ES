using System;
using System.Collections.Generic;
using System.Text;
using EventsAndCommands;

namespace CommandHandlers
{
    public interface ICommandHandler
    {
        Type CommandType { get; }
        void Handle(ICommand command);
        bool CommandIsCorrect(ICommand command);
    }
}
