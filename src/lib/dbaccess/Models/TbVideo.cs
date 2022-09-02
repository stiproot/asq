using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbVideo
    {
        public TbVideo()
        {
            TbFocusVideoMappings = new HashSet<TbFocusVideoMapping>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public short? Part { get; set; }
        public long VidId { get; set; }
        public long ImgId { get; set; }
        public string VideoGroupId { get; set; }

        public virtual TbUser CreationUser { get; set; }
        public virtual TbImg Img { get; set; }
        public virtual TbVid Vid { get; set; }
        public virtual ICollection<TbFocusVideoMapping> TbFocusVideoMappings { get; set; }
    }
}
