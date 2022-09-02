using System.Collections.Generic;

namespace DTO.Zoom.Meeting
{
    public class GetMeetingResponse
    {
        public string uuid{ get; set; }
        public int id{ get; set; }
        public string host_id{ get; set; }
        public string topic{ get; set; }
        public MeetingTypeEnu type{ get; set; }
        //waiting, started, finished
        public string status{ get; set; }
        // string
        public System.DateTime start_time{ get; set; }
        public int duration{ get; set; }
        public string timezone{ get; set; }
        // string
        public System.DateTime created_at{ get; set; }
        public string agenda{ get; set; } 
        public string start_url{ get; set; }
        public string join_url{ get; set; }
        public string password{ get; set; }
        public string h323_password{ get; set; }
        public string encrypted_password{ get; set; }
        public int pmi{ get; set; }
        public IEnumerable<dynamic> tracking_fields{ get; set; }
        public IEnumerable<dynamic> occurrences{ get; set; }
        public MeetingSettings settings{ get; set; }
        public MeetingRecurrence recurrence{ get; set; }
    }
}