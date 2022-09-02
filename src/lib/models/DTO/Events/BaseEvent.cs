using System;

namespace DTO.Events
{
    public class BaseEvent : IEvent
    {
        public Guid id{ get; set; }
        public DateTime event_date_utc{ get; set; }
    }
}