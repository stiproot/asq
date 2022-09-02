import { HostFilterConfigDto } from './host-filter-config-dto';
import { HostSummaryDto } from './host-summary-dto';

export class HostQueryDto{
    public display: string = '';
    public config: HostFilterConfigDto = null;
    public data: HostSummaryDto[] = [];
    public id: string = '';

    constructor(){ }
}
