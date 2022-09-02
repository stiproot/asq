import { BaseFocusMappingDto } from './base-focus-mapping-dto';

export class FocusMeetingMappingDto extends BaseFocusMappingDto{
    public meetingId: number;
    constructor(focusId: number, meetingId: number | null){
        super(focusId);
        this.meetingId = meetingId;
    }
}
