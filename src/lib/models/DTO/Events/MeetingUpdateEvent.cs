using DTO.Domain;

namespace DTO.Events
{
    public class MeetingUpdateEvent : BaseEvent
    {
        public System.Guid tracking_id{ get; set; }
        public MeetingDto meeting{ get; set; }
    }
}