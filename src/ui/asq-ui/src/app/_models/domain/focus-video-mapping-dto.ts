import { BaseFocusMappingDto } from './base-focus-mapping-dto';

export class FocusVideoMappingDto extends BaseFocusMappingDto{
    public videoId: number = 0;

    constructor(focusId: number, videoId: number){
        super(focusId);
        this.videoId = videoId;
    }
}
