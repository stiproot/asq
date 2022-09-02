import { BaseDomainDto } from './base-domain-dto';

export class MeetingReviewDto extends BaseDomainDto{
    public meetingUserMappingId: number;
    public review: string;
    public rating: number;
}
