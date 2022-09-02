using System;
using System.Collections.Generic;

namespace DTO.Zoom.Meeting
{
    public class CreateMeetingRequest
    {
        public string topic{ get; set; }
        public MeetingTypeEnu type{ get; set; }
        // string
        public DateTime start_time{ get; set; }
        public int duration{ get; set; }
        public string schedule_for{ get; set; }
        public string timezone{ get; set; }
        public string password{ get; set; }
        public string agenda{ get; set; }
        public IEnumerable<TrackingField> tracking_fields{ get; set; }
        public MeetingRecurrence recurrence{ get; set; }
        public MeetingSettings settings{ get; set; }
    }
}