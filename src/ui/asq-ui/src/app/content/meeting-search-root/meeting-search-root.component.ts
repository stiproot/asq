import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MeetingService } from '@app/content/meeting.service';
import { ProgressBarService } from '@app/_core/_services/progress-bar.service';
import { ContentFilterConfigDto } from '@app/_models/domain/content-filter-config-dto';
import { MeetingFilterConfigDto } from '@app/_models/domain/meeting-filter-config-dto';
import { MeetingSummaryDto } from '@app/_models/domain/meeting-summary-dto';

@Component({
  selector: 'app-meeting-search-root',
  templateUrl: './meeting-search-root.component.html',
  styleUrls: ['./meeting-search-root.component.css']
})
export class MeetingSearchRootComponent implements OnInit {

  timer: number;
  meetings: MeetingSummaryDto[] = [];
  filter: MeetingFilterConfigDto = null;

  constructor(
    private progressBarService: ProgressBarService,
    private activatedRoute: ActivatedRoute,
    private meetingService: MeetingService
  ) 
  { 
    this.filter = new MeetingFilterConfigDto();
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
    this.meetingService.getFilteredSummaries(this.filter)
      .subscribe(resp => {
        console.log('meeting-search-root', 'getFiltered', 'resp', resp);
        this.meetings = resp;
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
        const meetingFilter = new MeetingFilterConfigDto();
        meetingFilter.elastic = x.elastic;
        meetingFilter.estimatedDuration = x.estimatedDuration;
        meetingFilter.foci = x.foci;
        meetingFilter.creationUserName = x.name;
        meetingFilter.creationUserUniqueId = x.creationUserUniqueId;
        meetingFilter.startDateUtc = x.startDateUtc;
        meetingFilter.meetingStatusId = x.statusId;
        meetingFilter.title = x.title;
        meetingFilter.description = x.description;
        return meetingFilter
      })[0];
    
    this.clearTimer();
    this.getFiltered();
  }
}
