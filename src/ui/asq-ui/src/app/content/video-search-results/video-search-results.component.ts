import { Component, OnInit, Input } from '@angular/core';
import { VideoSummaryDto } from '@app/_models/domain/video-summary-dto';

@Component({
    selector: 'app-video-search-results',
    templateUrl: './video-search-results.component.html',
    styleUrls: [
        './video-search-results.component.css',
        './../../shared/shared.search-results.style.css'
    ]
})
export class VideoSearchResultsComponent implements OnInit {

    @Input() videos: VideoSummaryDto[] = null;

    constructor() { }

    ngOnInit(): void { }
}
