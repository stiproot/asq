using System;
using System.Collections.Generic;

namespace DTO.Domain
{
    public class VideoDto : BaseDomainDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public short? Part { get; set; }
        public long VidId { get; set; }
        public long ImgId { get; set; }
        public string VideoGroupId { get; set; }

        public UserDto CreationUser{ get; set; }
        public ImgDto Img{ get; set; }
        public VidDto Vid{ get; set; }
        public ICollection<FocusVideoMappingDto> Foci{ get; set; }

        new public bool Validate(bool throwException = false)
        {
            var invalid = base.Validate()
                && string.IsNullOrEmpty(this.Title)
                || string.IsNullOrEmpty(this.Description)
                || (this.Img == null && this.ImgId <= 0)
                || (this.Vid == null && this.VidId <= 0);

            if(invalid && throwException)
            {
                throw new Exception($"Invalid {nameof(VideoDto)} structure");
            }

            return invalid;
        }
    }
}