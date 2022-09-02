import { BaseDomainDto } from './base-domain-dto';

export class VidDto extends BaseDomainDto{

    public url: string = null;
    public filePath: string = null;

    constructor(){
        super();
    }
}
