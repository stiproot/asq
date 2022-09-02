using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbBlogPost
    {
        public TbBlogPost()
        {
            TbFocusBlogPostMappings = new HashSet<TbFocusBlogPostMapping>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long ImgId { get; set; }

        public virtual TbUser CreationUser { get; set; }
        public virtual TbImg Img { get; set; }
        public virtual ICollection<TbFocusBlogPostMapping> TbFocusBlogPostMappings { get; set; }
    }
}
