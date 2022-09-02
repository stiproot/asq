import { BaseDomainDto } from './base-domain-dto';

export class MeetingRecordingDto extends BaseDomainDto{
    public fileName: string = null;
    public path: string = null;
    public part: number = null;

    constructor(){ super(); }
}
