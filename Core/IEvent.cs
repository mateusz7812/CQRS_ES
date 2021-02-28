using System;

namespace Core
{
    public interface IEvent : IHandleable
    {
        Guid EventGuid { get; set; }
        Guid ItemGuid { get; }
    }
}
