using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbHost
    {
        public TbHost()
        {
            TbFocusHostMappings = new HashSet<TbFocusHostMapping>();
            TbMeetings = new HashSet<TbMeeting>();
            TbUsers = new HashSet<TbUser>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public bool? IsConsultant { get; set; }
        public string Company { get; set; }
        public string CareerSummary { get; set; }
        public long ExtUserId { get; set; }

        public virtual TbExtZoomUser ExtUser { get; set; }
        public virtual ICollection<TbFocusHostMapping> TbFocusHostMappings { get; set; }
        public virtual ICollection<TbMeeting> TbMeetings { get; set; }
        public virtual ICollection<TbUser> TbUsers { get; set; }
    }
}
