import { BaseDomainDto } from './base-domain-dto';

export class BlogPostSummaryDto extends BaseDomainDto{
    public title: string = '';
    public description: string = '';
    public creationUserUniqueId: string = null;
    public creationUserName: string = '';
    public creationUserSurname: string = '';
    public creationUserUsername: string = '';
    public thumbnailUrl: string = '';
    public creationUserThumbnailUrl: string = '';

    constructor(){ super(); }
}
