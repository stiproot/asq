import { BaseDomainDto } from './base-domain-dto';

export class FocusDto extends BaseDomainDto{

    public description: string = null;

    constructor(){ super(); }
}
