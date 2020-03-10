using System;
using System.Collections.Generic;
using CommandHandlers;
using EventsAndCommands;

namespace CommandBus
{
    public class DefaultCommandBus : ICommandBus
    {
        private readonly Dictionary<Type, ICommandHandler> _commandHandlersByCommandType = new Dictionary<Type, ICommandHandler>();
        private readonly Queue<ICommand> _commands = new Queue<ICommand>();

        public DefaultCommandBus(){}

        public void AddCommand(ICommand command) => _commands.Enqueue(command);

        public void AddCommandHandler(ICommandHandler commandHandler) 
            => _commandHandlersByCommandType.Add(commandHandler.CommandType, commandHandler);

        public void HandleNext()
        {
            var command = _commands.Dequeue();
            var handler = _commandHandlersByCommandType[command.GetType()];
            if (handler.CommandIsCorrect(command)) handler.Handle(command);
        }

        public bool IsBusEmpty => _commands.Count == 0;
    }
}
