export class MeetingSummaryQueryBuilderConfigDto{
    public meetingStatusId: number = 1;
    public creationUserUniqueId: string = null;

    constructor(){ }

    public generateCacheKey = () => `MeetingSummaryQueryBuilderConfig::MeetingStatusId:${this.meetingStatusId};CreationUserUniqueId:${this.creationUserUniqueId}`;
}
