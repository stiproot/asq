using System;
using System.Collections.Generic;

namespace DTO.Domain
{
    public class BlogPostDto : BaseDomainDto
    {
        public string Title{ get; set; }
        public string Content{ get; set; }
        public long ImgId{ get; set; }
        public ICollection<FocusBlogPostMappingDto> Foci{ get; set; }

        public UserDto CreationUser{ get; set; }
        public ImgDto Img{ get; set; }

        new public bool Validate(bool throwException = false)
        {
            var invalid = base.Validate()
                && string.IsNullOrEmpty(this.Title)
                || string.IsNullOrEmpty(this.Content)
                || CreationUserId <= 0
                || (this.Img == null && this.ImgId <= 0);

            if(invalid && throwException)
            {
                throw new Exception($"Invalid {nameof(BlogPostDto)} structure");
            }

            return invalid;
        }
    }
}