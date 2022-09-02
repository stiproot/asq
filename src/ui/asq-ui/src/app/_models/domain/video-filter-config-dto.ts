export class VideoFilterConfigDto{
    public creationUserUniqueId: string | null = null;
    public foci: number[] = [];

    public elastic: string = null;
    public creationUserName: string = null;
    public title: string = null;
    public description: string = null;
    public take: number = null;

    constructor(){ }
}
