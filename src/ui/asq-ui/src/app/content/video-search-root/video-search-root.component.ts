import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProgressBarService } from '@app/_core/_services/progress-bar.service';
import { ContentFilterConfigDto } from '@app/_models/domain/content-filter-config-dto';
import { VideoFilterConfigDto } from '@app/_models/domain/video-filter-config-dto';
import { VideoSummaryDto } from '@app/_models/domain/video-summary-dto';
import { VideoService } from '../video.service';

@Component({
  selector: 'app-video-search-root',
  templateUrl: './video-search-root.component.html',
  styleUrls: ['./video-search-root.component.css']
})
export class VideoSearchRootComponent implements OnInit {

  timer: number;
  videos: VideoSummaryDto[] = [];
  filter: VideoFilterConfigDto = null;

  constructor(
    private progressBarService: ProgressBarService,
    private activatedRoute: ActivatedRoute,
    private videoService: VideoService
  ) 
  { 
    this.filter = new VideoFilterConfigDto();
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
    this.videoService.getFilteredSummaries(this.filter)
      .subscribe(resp => {
        console.log('video-search-root', 'getFiltered', 'resp', resp);
        this.videos = resp;
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
        const videoFilter = new VideoFilterConfigDto();
        videoFilter.elastic = x.elastic;
        videoFilter.foci = x.foci;
        videoFilter.creationUserName = x.name;
        videoFilter.creationUserUniqueId = x.creationUserUniqueId;
        videoFilter.title = x.title;
        videoFilter.description = x.description;
        return videoFilter
      })[0];
    
    this.clearTimer();
    this.getFiltered();
  }

}
