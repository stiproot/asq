using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbNotification
    {
        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public long UserId { get; set; }
        public bool Seen { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string ImgUrl { get; set; }
        public string VideoUrl { get; set; }
        public string MeetingUrl { get; set; }
        public string ExtMeetingUrl { get; set; }
        public short NotificationTypeId { get; set; }

        public virtual TbNotificationType NotificationType { get; set; }
        public virtual TbUser User { get; set; }
    }
}
