using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbUser
    {
        public TbUser()
        {
            TbBlogPosts = new HashSet<TbBlogPost>();
            TbFocusUserMappings = new HashSet<TbFocusUserMapping>();
            TbMeetingUserMappings = new HashSet<TbMeetingUserMapping>();
            TbMeetings = new HashSet<TbMeeting>();
            TbNotifications = new HashSet<TbNotification>();
            TbVideos = new HashSet<TbVideo>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public short AccountTypeId { get; set; }
        public long ImgId { get; set; }
        public long? HostId { get; set; }
        public short TimezoneId { get; set; }
        public long? PaymentInfoId { get; set; }

        public virtual TbAccountType AccountType { get; set; }
        public virtual TbHost Host { get; set; }
        public virtual TbImg Img { get; set; }
        public virtual TbPaymentInfo PaymentInfo { get; set; }
        public virtual TbTimezone Timezone { get; set; }
        public virtual ICollection<TbBlogPost> TbBlogPosts { get; set; }
        public virtual ICollection<TbFocusUserMapping> TbFocusUserMappings { get; set; }
        public virtual ICollection<TbMeetingUserMapping> TbMeetingUserMappings { get; set; }
        public virtual ICollection<TbMeeting> TbMeetings { get; set; }
        public virtual ICollection<TbNotification> TbNotifications { get; set; }
        public virtual ICollection<TbVideo> TbVideos { get; set; }
    }
}
