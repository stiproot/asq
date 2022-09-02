export class BlogSummaryQueryBuilderConfigDto{
    public creationUserUniqueId: string = null;

    constructor(){ }

    public generateCacheKey = () => `BlogSummaryQueryBuilderConfigDto::CreationUserUniqueId:${this.creationUserUniqueId}`;
}
