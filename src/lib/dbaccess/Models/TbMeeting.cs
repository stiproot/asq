using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbMeeting
    {
        public TbMeeting()
        {
            TbExtZoomWebHooks = new HashSet<TbExtZoomWebHook>();
            TbFocusMeetingMappings = new HashSet<TbFocusMeetingMapping>();
            TbMeetingRecordings = new HashSet<TbMeetingRecording>();
            TbMeetingUserMappings = new HashSet<TbMeetingUserMapping>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDateUtc { get; set; }
        public int EstimatedDuration { get; set; }
        public long HostId { get; set; }
        public short MeetingStatusId { get; set; }
        public long ImgId { get; set; }
        public long ExtMeetingId { get; set; }
        public short TimezoneId { get; set; }

        public virtual TbUser CreationUser { get; set; }
        public virtual TbExtZoomMeeting ExtMeeting { get; set; }
        public virtual TbHost Host { get; set; }
        public virtual TbImg Img { get; set; }
        public virtual TbMeetingStatus MeetingStatus { get; set; }
        public virtual TbTimezone Timezone { get; set; }
        public virtual ICollection<TbExtZoomWebHook> TbExtZoomWebHooks { get; set; }
        public virtual ICollection<TbFocusMeetingMapping> TbFocusMeetingMappings { get; set; }
        public virtual ICollection<TbMeetingRecording> TbMeetingRecordings { get; set; }
        public virtual ICollection<TbMeetingUserMapping> TbMeetingUserMappings { get; set; }
    }
}
