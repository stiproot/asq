using System;

namespace DTO.Notification
{
    public class NotificationConfig
    {
        public string ToEmailAddress{ get; set; }
        public string ToUsername{ get; set; }
        public string ToName{ get; set; }
        public string ToSurname{ get; set; }
        public NotificationTypeEnu NotificationType{ get; set; }

        // Email confirmation
        public Guid ToUniqueId{ get; set; }

        // Meeting registration
        public string ParticipantUsername{ get; set; }
        public string ParticipantName{ get; set; }
        public string ParticipantSurname{ get; set; }

        // Meeting creation
        public string ExtMeetingStartUrl{ get; set; }
        public long MeetingHostId{ get; set; }
        public Guid MeetingHostUniqueId{ get; set; }
        public long MeetingId{ get; set; }
        public string MeetingTitle{ get; set; }
        public string MeetingThumbnailUrl{ get; set; }
        public string MeetingUrl{ get; set; }
        public Guid MeetingUniqueId{ get; set; }
        public string MeetingTimezone{ get; set; }
        public string MeetingStartDatetime{ get; set; }

        // Notification
        public long UserId{ get; set; }
    }
}