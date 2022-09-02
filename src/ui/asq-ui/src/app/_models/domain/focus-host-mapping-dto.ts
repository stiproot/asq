import { BaseFocusMappingDto } from './base-focus-mapping-dto';

export class FocusHostMappingDto extends BaseFocusMappingDto{
    public hostId: number = 0;

    constructor(focusId: number, hostId: number){
        super(focusId);
        this.hostId = hostId;
    }
}
