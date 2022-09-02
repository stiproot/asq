using System;
using System.Collections.Generic;

namespace DTO.Domain
{
    public class MeetingReviewDto: BaseDomainDto
    {
        public long MeetingUserMappingId{ get; set; }
        public string Review{ get; set; }
        public float Rating{ get; set; }
    }
}