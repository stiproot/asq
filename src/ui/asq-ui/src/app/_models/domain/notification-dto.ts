import { BaseDomainDto } from './base-domain-dto';

export class NotificationDto extends BaseDomainDto{
    public userId: number;
    public seen: boolean;
    public title: string;
    public message: string;
    public imgUrl: string;
    public videoUrl: string;
    public meetingUrl: string;
    public extMeetingUrl: string;
    public notifcationType: number;

    public NotifciationDto() { }
}