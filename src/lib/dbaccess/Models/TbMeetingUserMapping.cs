using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbMeetingUserMapping
    {
        public TbMeetingUserMapping()
        {
            TbMeetingReviews = new HashSet<TbMeetingReview>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public long UserId { get; set; }
        public long MeetingId { get; set; }

        public virtual TbMeeting Meeting { get; set; }
        public virtual TbUser User { get; set; }
        public virtual ICollection<TbMeetingReview> TbMeetingReviews { get; set; }
    }
}
