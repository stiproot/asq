namespace DTO.Events
{
    public class MeetingRecordingDownloadEvent : BaseEvent
    {
        public System.Guid tracking_id{ get; set; }
        public DTO.Domain.MeetingDto meeting{ get; set; }
    }
}