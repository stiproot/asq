import { BaseDomainDto } from './base-domain-dto';
import { UserDto } from './user-dto';
import { ImgDto } from './img-dto';
import { FocusBlogPostMappingDto } from './focus-blog-post-mapping-dto';

export class BlogPostDto extends BaseDomainDto{
    public title: string = '';
    public content: string = '';
    public creationUserId: number = 0;
    public imgId: number = 0;
    public foci: FocusBlogPostMappingDto[] = null;

    public creationUser: UserDto = null;
    public img: ImgDto = null;

    constructor(){
        super();
    }
}
