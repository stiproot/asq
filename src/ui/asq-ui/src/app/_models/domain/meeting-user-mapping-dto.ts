import { BaseDomainDto } from './base-domain-dto';
import { UserDto } from './user-dto';
import { MeetingReviewDto } from './meeting-review-dto';

export class MeetingUserMappingDto extends BaseDomainDto{
    public meetingId: number;
    public userId: number;
    public inactive: boolean;
    public user: UserDto;

    public reviews: MeetingReviewDto[];
}

