import * as moment from 'moment';

export class ContentFilterConfigDto{

    // general filters
    public foci: number[] = [];
    public elastic: string = null;
    public take: number = null;
    public name: string = null;
    public title: string = null;

    // meeting specific filters
    public statusId: number = null;
    public startDateUtc: moment.Moment = null;
    public estimatedDuration: number = null;
    public timezoneId: number = 0;
    public creationUserUniqueId: string = null;
    public description: string = null;

    // blog specific filters
    public authorUniqueId: string = null;

    constructor(){ }
}
