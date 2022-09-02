using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbTimezone
    {
        public TbTimezone()
        {
            TbMeetings = new HashSet<TbMeeting>();
            TbUsers = new HashSet<TbUser>();
        }

        public short Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Display { get; set; }
        public byte UtcOffset { get; set; }
        public string ExtCode { get; set; }

        public virtual ICollection<TbMeeting> TbMeetings { get; set; }
        public virtual ICollection<TbUser> TbUsers { get; set; }
    }
}
