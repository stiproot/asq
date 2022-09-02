using System;

namespace DTO.Domain
{
    public class BlogSummaryQueryBuilderConfigDto
    {
        public Guid? CreationUserUniqueId{ get; set; }

        public string GenerateCacheKey() 
            => $"{nameof(BlogSummaryQueryBuilderConfigDto)}::{nameof(CreationUserUniqueId)}:{this.CreationUserUniqueId};";
    }
}