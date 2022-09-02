import { Component, OnInit, Input } from '@angular/core';
import { HostSummaryDto } from '@app/_models/domain/host-summary-dto';

@Component({
    selector: 'app-profile-search-results',
    templateUrl: './profile-search-results.component.html',
    styleUrls: [
        './profile-search-results.component.css',
        './../../shared/shared.search-results.style.css'
    ]
})
export class ProfileSearchResultsComponent implements OnInit {

    @Input() users: HostSummaryDto[] = null;

    constructor() { }

    ngOnInit(): void { }
}
