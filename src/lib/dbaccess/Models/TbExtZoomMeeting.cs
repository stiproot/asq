using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbExtZoomMeeting
    {
        public TbExtZoomMeeting()
        {
            TbExtZoomMeetingRecordings = new HashSet<TbExtZoomMeetingRecording>();
            TbMeetings = new HashSet<TbMeeting>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Payload { get; set; }

        public virtual ICollection<TbExtZoomMeetingRecording> TbExtZoomMeetingRecordings { get; set; }
        public virtual ICollection<TbMeeting> TbMeetings { get; set; }
    }
}
