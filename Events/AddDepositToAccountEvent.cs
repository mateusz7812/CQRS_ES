using System;
using Core;
namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { }
}
namespace Events
{
    public class AddDepositToAccountEvent: IEvent
    {
        public Guid EventGuid { get; init; }
        public Guid ItemGuid { get; init; }
        public Guid DepositId { get; init; }
    }
}
