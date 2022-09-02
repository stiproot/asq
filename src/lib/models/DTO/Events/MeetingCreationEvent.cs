using System;
using DTO.Domain;

namespace DTO.Events
{
    public class MeetingCreationEvent : BaseEvent
    {
        public Guid tracking_id{ get; set; }
        public MeetingDto meeting{ get; set; }
    }
}