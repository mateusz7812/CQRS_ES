using CommandHandlers;
using CommandHandlers.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using EventsAndCommands;

namespace CommandBus
{
    public interface ICommandBus
    {
        void AddCommandHandler(ICommandHandler commandHandler);
        void AddCommand(ICommand command);
        void HandleNext();
        bool IsBusEmpty { get; }
    }
}
