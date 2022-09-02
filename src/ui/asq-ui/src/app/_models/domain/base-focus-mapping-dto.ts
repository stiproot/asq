import { BaseDomainDto } from './base-domain-dto';
import { FocusDto } from './focus-dto';

export class BaseFocusMappingDto extends BaseDomainDto{
    public focusId: number = null;

    public focus: FocusDto = null;

    constructor(focusId: number){
        super();
        this.focusId = focusId;
    }
}
