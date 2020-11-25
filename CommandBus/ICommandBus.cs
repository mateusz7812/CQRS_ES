using CommandHandlers;
using CommandHandlers.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using Bus;
using Commands;

namespace CommandBus
{
    public interface ICommandBus: IBus<ICommand>
    {
    }
}
