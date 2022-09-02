namespace DTO.Zoom.Meeting
{
    public class GetMeetingRecordingsResponse
    {
        public string uuid{ get; set; }
        public long id{ get; set; }
        public string account_id{ get; set; }
        public string host_id{ get; set; }
        public string topic{ get; set; }
        public System.DateTime start_time{ get; set; }
        //public string start_time{ get; set; }
        public int duration{ get; set; }
        public long total_size{ get; set; }
        public MeetingTypeEnu type{ get; set; }
        //public string type{ get; set; }
        public int recording_count{ get; set; }
        public System.Collections.Generic.IEnumerable<MeetingRecordingFile> recording_files{ get; set; }
    }

    public enum FileTypeEnum
    {
        MP4,
        M4A,
        TIMELINE,
        TRANSCRIPT,
        CHAT,
        CC,
        CSV
    }

    public class MeetingRecordingFile
    {
        public string id{ get; set; }
        //public string meeting_id{ get; set; }
        public System.DateTime recording_start{ get; set; }
        //public string recording_start{ get; set; }
        public System.DateTime recording_end{ get; set; }
        //public string recording_end{ get; set; }
        //public FileTypeEnum file_type{ get; set; }
        public string file_type{ get; set; }
        public string file_extension{ get; set; }
        public long file_size{ get; set; }
        public string play_url{ get; set; }
        public string download_url{ get; set; }
        public string status{ get; set; }
        public string deleted_time{ get; set; }
        public string recording_type{ get; set; }
    }
}