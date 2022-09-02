import * as uuid from 'uuid';
import * as moment from 'moment';

export class BaseDomainDto{
    public id: number = 0;
    public uniqueId: string = uuid.v4();
    public creationDateUtc: moment.Moment = moment.utc();
    public creationUserId: number = 0;
    public inactive: boolean = false;

    constructor(){ }
}
