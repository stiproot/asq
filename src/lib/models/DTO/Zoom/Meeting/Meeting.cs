using System;

namespace DTO.Zoom.Meeting
{
    public class Meeting
    {
        public string uuid{ get; set; }
        public string id{ get; set; }
        public string host_id{ get; set; }
        public string topic{ get; set; }
        public MeetingTypeEnu type{ get; set; }
        public DateTime start_time{ get; set; }
        public int duration{ get; set; }
        public string timezone{ get; set; }
        public DateTime? created_at{ get; set; }
        public string join_url{ get; set; }
        public string agenda{ get; set; }

        // webhooks

        // meeting.created
        public string password{ get; set; }
        // meeting.participant_joined 
        // meeting.participant_left 
        // meeting.participant_admitted 
        // meeting.participant_jbh_waiting 
        // meeting.sharing_started 
        // meeting.sharing_ended 
        public Participant participant{ get; set; }
    }
}