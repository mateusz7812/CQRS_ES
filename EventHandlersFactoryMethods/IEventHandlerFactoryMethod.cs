using System;
using System.Collections.Generic;
using System.Text;
using EventHandlers;
using Events;

namespace EventHandlersFactoryMethods
{
    public interface IEventHandlerFactoryMethod
    {
        IEventHandler CreateHandler(IEvent @event);
    }
}
