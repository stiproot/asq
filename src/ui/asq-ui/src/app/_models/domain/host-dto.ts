import { BaseDomainDto } from './base-domain-dto';
import { FocusHostMappingDto } from './focus-host-mapping-dto';

export class HostDto extends BaseDomainDto{
    public company: string = null;
    public careerSummary: string = null;
    public isConsultant: boolean = false;
    public specialities: FocusHostMappingDto[] = null;

    constructor(){ super(); }
}
