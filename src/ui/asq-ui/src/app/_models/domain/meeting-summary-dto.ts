import { BaseDomainDto } from './base-domain-dto';
import * as moment from 'moment';

export class MeetingSummaryDto extends BaseDomainDto{
    public title: string = null;
    public description: string = null;
    public meetingStatusId: number = 0;
    public statusDescription: string = null;
    public startDateUtc: moment.Moment = null;
    public estimatedDuration: number = null;
    public timezoneId: number = 0;
    public creationUserUniqueId: string = null;
    public creationUserName: string = null;
    public creationUserSurname: string = null;
    public creationUserUsername: string = null;
    public thumbnailUrl: string = null;
    public creationUserThumbnailUrl: string = null;
    public hasPassed: boolean = null;
    public hasRecordings: boolean = null; 

    constructor(){ super(); }
}
