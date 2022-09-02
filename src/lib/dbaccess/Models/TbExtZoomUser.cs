using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbExtZoomUser
    {
        public TbExtZoomUser()
        {
            TbHosts = new HashSet<TbHost>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Payload { get; set; }

        public virtual ICollection<TbHost> TbHosts { get; set; }
    }
}
