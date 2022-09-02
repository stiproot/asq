using DTO.Domain;
using System;

namespace DTO.Events
{
    public class MeetingParticipationEvent : BaseEvent
    {
        public Guid meeting_id{ get; set; }
        public long user_id{ get; set; }
        public MeetingDto meeting{ get; set; }
        public bool register{ get; set; }
    }
}