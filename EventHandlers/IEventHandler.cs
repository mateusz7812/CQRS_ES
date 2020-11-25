using System;
using System.Collections.Generic;
using System.Text;
using Bus;
using Events;

namespace EventHandlers
{
    public interface IEventHandler: IHandler<IEvent>
    {
    }
}
