using System;

namespace DTO.Zoom.Meeting
{
    // this is created and used within the context of webhooks
    public class Participant
    {
        public string user_id{ get; set; }
        public string user_name{ get; set; }
        public string id{ get; set; }
        public string join_time{ get; set; }
    }
}