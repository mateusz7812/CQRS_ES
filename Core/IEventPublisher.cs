using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface IEventPublisher
    {
        public void Publish(IEvent @event);
    }
}
