using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbExtZoomMeetingRecording
    {
        public TbExtZoomMeetingRecording()
        {
            TbMeetingRecordings = new HashSet<TbMeetingRecording>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Payload { get; set; }
        public long ExtMeetingId { get; set; }

        public virtual TbExtZoomMeeting ExtMeeting { get; set; }
        public virtual ICollection<TbMeetingRecording> TbMeetingRecordings { get; set; }
    }
}
