using System;
using System.Collections.Generic;

namespace DTO.Domain
{
    public class MeetingUserMappingDto: BaseDomainDto
    {
        public long MeetingId{ get; set; }
        public long UserId{ get; set; }

        public UserDto User{ get; set; }
        public ICollection<MeetingReviewDto> Reviews{ get; set; }

        public MeetingUserMappingDto(): base(){ }
        public MeetingUserMappingDto(long meetingId, long userId): base()
        { 
            this.MeetingId = meetingId;
            this.UserId = userId;
        }
    }
}