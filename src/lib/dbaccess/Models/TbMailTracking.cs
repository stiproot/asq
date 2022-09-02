using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbMailTracking
    {
        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Request { get; set; }
        public string Tracking { get; set; }
        public bool Failed { get; set; }
        public short StatusId { get; set; }
    }
}
