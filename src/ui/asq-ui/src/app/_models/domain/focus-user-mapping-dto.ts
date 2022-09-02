import { BaseFocusMappingDto } from './base-focus-mapping-dto';

export class FocusUserMappingDto extends BaseFocusMappingDto{
    public userId: number = 0;

    constructor(focusId: number, userId: number){
        super(focusId);
        this.userId = userId;
    }
}
