using System;
using Core;

namespace Events
{
    public class CreateAccountEvent : IEvent
    {
        public Guid EventGuid { get; init; }
        public Guid ItemGuid { get; init; }
        public string AccountName { get; init; }
    }
}
