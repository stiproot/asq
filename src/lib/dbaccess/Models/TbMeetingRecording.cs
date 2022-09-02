using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbMeetingRecording
    {
        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public short Part { get; set; }
        public long MeetingId { get; set; }
        public long ExtMeetingRecordingId { get; set; }

        public virtual TbExtZoomMeetingRecording ExtMeetingRecording { get; set; }
        public virtual TbMeeting Meeting { get; set; }
    }
}
