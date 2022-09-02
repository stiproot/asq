export class VideoSummaryQueryBuilderConfigDto{
    public creationUserUniqueId: string = null;

    constructor(){ }

    public generateCacheKey = () => `VideoSummaryQueryBuilderConfigDto::AuthorId:${this.creationUserUniqueId}`;
}
