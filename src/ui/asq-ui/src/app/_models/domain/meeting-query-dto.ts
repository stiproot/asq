import { Observable } from 'rxjs';
import { MeetingFilterConfigDto } from './meeting-filter-config-dto';
import { MeetingSummaryDto } from './meeting-summary-dto';

export class MeetingQueryDto{
    public display: string = '';
    public config: MeetingFilterConfigDto = null;
    public url: string = '';
    public data$: Observable<MeetingSummaryDto[]>;
    public id: string = '';

    constructor(){ }
}
