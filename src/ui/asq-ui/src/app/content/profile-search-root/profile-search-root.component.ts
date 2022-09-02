import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProgressBarService } from '@app/_core/_services/progress-bar.service';
import { ContentFilterConfigDto } from '@app/_models/domain/content-filter-config-dto';
import { HostFilterConfigDto } from '@app/_models/domain/host-filter-config-dto';
import { HostSummaryDto } from '@app/_models/domain/host-summary-dto';
import { HostService } from '../host.service';

@Component({
  selector: 'app-profile-search-root',
  templateUrl: './profile-search-root.component.html',
  styleUrls: ['./profile-search-root.component.css']
})
export class ProfileSearchRootComponent implements OnInit {

    timer: number;
    users: HostSummaryDto[] = [];
    filter: HostFilterConfigDto = null;

    constructor(
        private progressBarService: ProgressBarService,
        private activatedRoute: ActivatedRoute,
        private hostService: HostService
    ) 
    { 
        this.filter = new HostFilterConfigDto();
        this.filter.take = 25;
    }

    ngOnInit(): void {
        this.activatedRoute.queryParams.subscribe(q => {
            const query = q['q'];
            this.filter.elastic = query;
            this.clearTimer();
            if(query && query.length >= 3){
                this.setTimer();
            }
        });
    }

    private setTimer(): void{
        this.progressBarService.showProgressBar();
        this.timer = setTimeout(() => {
        this.getFiltered();
        }, 2200);
    }

    private getFiltered(): void{
        this.hostService.getFilteredSummaries(this.filter)
        .subscribe(resp => {
            console.log('meeting-search-root', 'getFiltered', 'resp', resp);
            this.users = resp;
            this.progressBarService.hideProgressBarDelayed();
        });
    }

    private clearTimer(): void{
        clearTimeout(this.timer);
        this.progressBarService.hideProgressBar();
    }

    onSearchCriteriaFilterClick(event: ContentFilterConfigDto): void{
        this.progressBarService.showProgressBar();
        this.filter = [event].map(x => 
        {
            const filter = new HostFilterConfigDto();
            filter.elastic = x.elastic;
            filter.foci = x.foci;
            filter.name = x.name;
            return filter
        })[0];
        
        this.clearTimer();
        this.getFiltered();
    }

}
