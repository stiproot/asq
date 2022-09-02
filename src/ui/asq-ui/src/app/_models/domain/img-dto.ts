import { BaseDomainDto } from './base-domain-dto';

export class ImgDto extends BaseDomainDto{
    // public data: File = null;
    public data: ArrayBuffer = null;
    // public base64: string = null;
    public path: string = null;
    public thumbnailUrl: string = null;
    public fileName: string = null;

    constructor(){
        super();
    }

    // public validate(throwError: boolean = false): boolean{
        // if (this.path != null && this.fileName != null){
            // return true;
        // }
        // if (throwError){
            // throw new Error('Invalid img structure');
        // }
        // return false;
    // }
}
