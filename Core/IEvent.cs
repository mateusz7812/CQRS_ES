using System;

namespace Core
{
    public interface IEvent : IHandleable
    {
        Guid EventGuid { get; }
        Guid ItemGuid { get; }
    }
}
