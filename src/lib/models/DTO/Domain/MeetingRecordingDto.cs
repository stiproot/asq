using DTO.Domain.Ext.Zoom;

namespace DTO.Domain
{
    public class MeetingRecordingDto : BaseDomainDto
    {
        public string FileName{ get; set; }
        public string Path{ get; set; }
        public short Part{ get; set; }
        public long ExtMeetingRecordingId{ get; set; }
        public long MeetingId{ get; set; }

        //public ExtZoomMeetingRecordingDto ExtMeetingRecording{ get; set; }
        //public MeetingDto Meeting{ get; set; }
    }
}