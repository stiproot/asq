using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbFocus
    {
        public TbFocus()
        {
            TbFocusBlogPostMappings = new HashSet<TbFocusBlogPostMapping>();
            TbFocusHostMappings = new HashSet<TbFocusHostMapping>();
            TbFocusMeetingMappings = new HashSet<TbFocusMeetingMapping>();
            TbFocusUserMappings = new HashSet<TbFocusUserMapping>();
            TbFocusVideoMappings = new HashSet<TbFocusVideoMapping>();
        }

        public short Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TbFocusBlogPostMapping> TbFocusBlogPostMappings { get; set; }
        public virtual ICollection<TbFocusHostMapping> TbFocusHostMappings { get; set; }
        public virtual ICollection<TbFocusMeetingMapping> TbFocusMeetingMappings { get; set; }
        public virtual ICollection<TbFocusUserMapping> TbFocusUserMappings { get; set; }
        public virtual ICollection<TbFocusVideoMapping> TbFocusVideoMappings { get; set; }
    }
}
