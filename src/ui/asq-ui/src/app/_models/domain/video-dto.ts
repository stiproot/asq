import { BaseDomainDto } from './base-domain-dto';
import { UserDto } from './user-dto';
import { ImgDto } from './img-dto';
import { VidDto } from './vid-dto';
import { FocusVideoMappingDto } from './focus-video-mapping-dto';

export class VideoDto extends BaseDomainDto{
    public title: string = '';
    public description: string = '';
    public fileName: string = '';
    public filePath: number = 0;
    public imgId: number = 0;
    public vidId: number = 0;
    public foci: FocusVideoMappingDto[] = null;

    public user: UserDto = null;
    public img: ImgDto = null;
    public vid: VidDto = null;

    constructor(){
        super();
    }
}
