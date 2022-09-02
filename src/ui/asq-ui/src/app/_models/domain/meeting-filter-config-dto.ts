import * as moment from 'moment';

export class MeetingFilterConfigDto{
    public meetingStatusId: number = null;
    public startDateUtc: moment.Moment = null;
    public estimatedDuration: number = null;
    public timezoneId: number = 0;
    public creationUserUniqueId: string = null;
    public foci: number[] = [];
    public creationUserName: string = null;
    public elastic: string = null;
    public title: string = null;
    public description: string = null;
    public take: number = null;

    constructor(){ }
}
