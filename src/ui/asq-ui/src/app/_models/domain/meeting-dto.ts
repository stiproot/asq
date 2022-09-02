import { BaseDomainDto } from './base-domain-dto';
import { BaseExtDto } from './base-ext-dto';
import { FocusMeetingMappingDto } from './focus-meeting-mapping-dto';
import { HostDto } from './host-dto';
import { MeetingUserMappingDto } from './meeting-user-mapping-dto';
import { MeetingStatusDto } from './meeting-status-dto';
import { UserDto } from './user-dto';
import { ImgDto } from './img-dto';
import { TimezoneDto } from './timezone-dto';
import { MeetingRecordingDto } from './../domain/meeting-recording-dto';

export class MeetingDto extends BaseDomainDto{
    public title: string = '';
    public description: string = '';
    public startDateUtc: string = null;
    public estimatedDuration: number = null;
    public hostId: number = 0;
    public meetingStatusId: number = 1;
    public imgId: number = 0;
    public timezoneId: number = 0;

    public meetingStatus: MeetingStatusDto = null;
    public host: HostDto = null;
    public foci: FocusMeetingMappingDto[] = null;
    public participants: MeetingUserMappingDto[] = null;
    public img: ImgDto = null;
    public timezone: TimezoneDto = null;
    public creationUser: UserDto = null; //new
    public recordings: MeetingRecordingDto[] = null;
    public extMeeting: BaseExtDto = null
    public hasPassed: boolean = false;

    constructor(){
        super();
    }
}
