import { Observable } from 'rxjs';
import { VideoFilterConfigDto } from './video-filter-config-dto' 
import { VideoSummaryDto } from './video-summary-dto';

export class VideoQueryDto{
    public display: string = '';
    public config: VideoFilterConfigDto = null;
    public url: string = '';
    public data$: Observable<VideoSummaryDto[]>;
    public id: string = '';

    constructor(){ }
}
