using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbExtZoomWebHook
    {
        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public long MeetingId { get; set; }
        public string Payload { get; set; }
        public string EventType { get; set; }

        public virtual TbMeeting Meeting { get; set; }
    }
}
