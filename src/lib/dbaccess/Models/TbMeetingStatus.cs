using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbMeetingStatus
    {
        public TbMeetingStatus()
        {
            TbMeetings = new HashSet<TbMeeting>();
        }

        public short Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TbMeeting> TbMeetings { get; set; }
    }
}
