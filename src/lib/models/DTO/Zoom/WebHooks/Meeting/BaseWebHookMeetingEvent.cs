namespace DTO.Zoom.WebHook.Meeting
{
    public class BaseWebHookMeetingEvent
    {
        public string @event{get; set;}
        public long event_ts{ get; set; }
        public WebHookEventMeetingPayload payload{ get; set; }
    }
}