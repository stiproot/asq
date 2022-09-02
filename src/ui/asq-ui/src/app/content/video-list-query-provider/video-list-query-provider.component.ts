import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { VideoService } from '../video.service';
import { VideoQueryDto } from '../../_models/domain/video-query-dto';
import { VideoSummaryQueryBuilderConfigDto } from '../../_models/domain/video-summary-query-builder-config-dto';

@Component({
  selector: 'app-video-list-query-provider',
  templateUrl: './video-list-query-provider.component.html',
  styleUrls: ['./video-list-query-provider.component.css']
})
export class VideoListQueryProviderComponent implements OnInit {

  @Input() inputCreationUserUniqueId: string | null = null;
  private config: VideoSummaryQueryBuilderConfigDto = new VideoSummaryQueryBuilderConfigDto();

  // models
  queries$: Observable<VideoQueryDto[]>;

  constructor(
    private videoService: VideoService
  ) { }

  ngOnInit(): void {

    this.config.creationUserUniqueId = this.inputCreationUserUniqueId;

    this.buildQueries();
  }

  private buildQueries(): void{
    this.queries$ = this.videoService.buildSummaryQueries(this.config);
  }
}
