using System;
using System.Text.Json;

namespace Core
{
    public class ErrorEvent: IEvent
    {
        public ErrorEvent(Guid eventGuid, Guid itemGuid, object errorData, string errorMessage)
        {
            EventGuid = eventGuid;
            ItemGuid = itemGuid;
            ErrorDataJson = JsonSerializer.Serialize(errorData);
            ErrorMessage = errorMessage;
        }

        public Guid EventGuid { get; set; }
        public Guid ItemGuid { get; }
        public string ErrorDataJson { get; }
        public string ErrorMessage { get; }
    }
}
