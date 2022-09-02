using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbMeetingReview
    {
        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public long MeetingUserMappingId { get; set; }
        public string Review { get; set; }
        public float Rating { get; set; }

        public virtual TbMeetingUserMapping MeetingUserMapping { get; set; }
    }
}
