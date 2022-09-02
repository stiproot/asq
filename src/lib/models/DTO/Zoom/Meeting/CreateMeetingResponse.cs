using System;
using System.Collections;
using System.Collections.Generic;

namespace DTO.Zoom.Meeting
{
    public class CreateMeetingResponse
    {
        public long id{ get; set; }
        public string topic{ get; set; }
        public MeetingTypeEnu type{ get; set; }
        // string
        public DateTime start_time{ get; set; }
        public int duration{ get; set; }
        public string timezone{ get; set; }
        // string
        public DateTime created_at{ get; set; }
        public string agenda{ get; set; } 
        public string start_url{ get; set; }
        public string join_url{ get; set; }
        public string password{ get; set; }
        public string h323_password{ get; set; }
        public int pmi{ get; set; }
        public IEnumerable<TrackingField> tracking_fields{ get; set; }
        //public IEnumerable<TrackingField> occurrences{ get; set; }
        public MeetingSettings settings{ get; set; }
        public MeetingRecurrence recurrence{ get; set; }
    }
}