using System;

namespace DTO.Domain
{
    public class VideoSummaryQueryBuilderConfigDto
    {
        public Guid? CreationUserUniqueId{ get; set; }

        public string GenerateCacheKey() 
            => $"{nameof(VideoSummaryQueryBuilderConfigDto)}::AuthorId:{this.CreationUserUniqueId};";
    }
}