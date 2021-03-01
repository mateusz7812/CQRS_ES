using System;
using Core;

namespace Events
{
    public class CreateAtmEvent:IEvent
    {
        public Guid EventGuid { get; set; }
        public Guid ItemGuid { get; init; }
    }
}