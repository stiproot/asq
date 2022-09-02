using DTO.Notification;

namespace DTO.Events
{
    public class SendNotificationEvent : BaseEvent
    {
        public NotificationConfig notification_config{ get; set; }
    }
}