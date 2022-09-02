using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbImg
    {
        public TbImg()
        {
            TbBlogPosts = new HashSet<TbBlogPost>();
            TbMeetings = new HashSet<TbMeeting>();
            TbUsers = new HashSet<TbUser>();
            TbVideos = new HashSet<TbVideo>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }

        public virtual ICollection<TbBlogPost> TbBlogPosts { get; set; }
        public virtual ICollection<TbMeeting> TbMeetings { get; set; }
        public virtual ICollection<TbUser> TbUsers { get; set; }
        public virtual ICollection<TbVideo> TbVideos { get; set; }
    }
}
