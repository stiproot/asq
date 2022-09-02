using System;
using DTO.Domain;

namespace DTO.Events
{
    public class AccountUpdateEvent : BaseEvent
    {
        public Guid tracking_id{ get; set; }
        public UserDto user{ get; set; }
    }
}