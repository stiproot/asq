import { BaseDomainDto } from './base-domain-dto';

export class HostSummaryDto extends BaseDomainDto{
    public username: string = '';
    public name: string = '';
    public surname: string = '';
    public thumbnailUrl: string = '';
    public foci: string[] = [];

    constructor(){ super(); }
}
