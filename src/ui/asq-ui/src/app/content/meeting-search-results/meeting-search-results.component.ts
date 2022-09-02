import { Component, OnInit, Input } from '@angular/core';
import { MeetingSummaryDto } from '@app/_models/domain/meeting-summary-dto';

@Component({
    selector: 'app-meeting-search-results',
    templateUrl: './meeting-search-results.component.html',
    styleUrls: [
            './meeting-search-results.component.css',
            './../../shared/shared.search-results.style.css'
        ]
})
export class MeetingSearchResultsComponent implements OnInit {

    @Input() meetings: MeetingSummaryDto[] = null;

    constructor() { }

    ngOnInit(): void { }
}
