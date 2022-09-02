import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { VideoService } from '../video.service';
import { VideoQueryDto } from '../../_models/domain/video-query-dto';
import { AuthService } from '@app/_core/_services/auth.service';

@Component({
  selector: 'app-video-list',
  templateUrl: './video-list.component.html',
  styleUrls: ['./video-list.component.css']
})
export class VideoListComponent implements OnInit{

  @Input() queries: VideoQueryDto[];

  constructor(
    public authService: AuthService,
    private router: Router,
    private videoService: VideoService
  ) { }

  ngOnInit(): void {
    console.log('video-list', 'ngOnInit', 'queries', this.queries);
    this.processQueries();
  }

  private processQueries(): void{
    this.queries.forEach((v, i) => {
      v.data$ = this.videoService.getFilteredSummaries(v.config);
      v.id = 'caro_' + i;
    });
  }

  private get isVideoView(): boolean
  {
    return true;
  }

  get isAddButtonVisible(): boolean{
    return this.authService.isLoggedIn && 
      this.authService.isHost && 
      this.isVideoView;
  }

  onAddVideoClick(event: Event): void{
    this.router.navigate(['/video/new'], { queryParamsHandling: 'merge' });
  }
}
