import { BaseDomainDto } from './base-domain-dto';

export class TimezoneDto extends BaseDomainDto{

    public display: string = null;
    public extCode: string = null;
    public utcOffset: number = null;

    constructor(){
        super();
    }
}
