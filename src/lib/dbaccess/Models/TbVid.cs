using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbVid
    {
        public TbVid()
        {
            TbVideos = new HashSet<TbVideo>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string FilePath { get; set; }
        public string Url { get; set; }

        public virtual ICollection<TbVideo> TbVideos { get; set; }
    }
}
