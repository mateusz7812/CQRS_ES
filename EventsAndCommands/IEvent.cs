using System;
using Bus;

namespace Events
{
    public interface IEvent: IHandleable
    {
        Guid EventGuid { get; }
        Guid ItemGuid { get; }
    }
}
