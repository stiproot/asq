import { BaseFocusMappingDto } from './base-focus-mapping-dto';

export class FocusBlogPostMappingDto extends BaseFocusMappingDto{
    public blogPostId: number = 0;

    constructor(focusId: number, blogPostId: number){
        super(focusId);
        this.blogPostId = blogPostId;
    }
}
