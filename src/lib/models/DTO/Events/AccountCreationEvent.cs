using DTO.Domain;

namespace DTO.Events
{
    public class AccountCreationEvent : BaseEvent
    {
        public System.Guid tracking_id{ get; set; }
        public UserDto user{ get; set; }
    }
}