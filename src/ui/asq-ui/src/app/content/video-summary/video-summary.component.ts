import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { AuthService } from '@app/_core/_services/auth.service';
import { VideoDetailDialogService } from '@app/_core/_services/dialog/video-detail-dialog.service';
import { ProfileDialogService } from '@app/_core/_services/dialog/profile-dialog.service';
import { RedirectService } from '@app/_core/_services/redirect.service';
import { VideoSummaryDto } from '@app/_models/domain/video-summary-dto';

@Component({
  selector: 'app-video-summary',
  templateUrl: './video-summary.component.html',
  styleUrls: [
    './video-summary.component.css',
    './../../shared/shared.content.style.css'
  ]
})
export class VideoSummaryComponent implements OnInit, OnDestroy {

  @Input() video: VideoSummaryDto = null;
  showProgressBar: boolean = false;

  constructor(
    private authService: AuthService,
    private redirectService: RedirectService,
    private profileDialogService: ProfileDialogService,
    private videoDetailDialogService: VideoDetailDialogService
  ) { }

  ngOnInit(): void { }

  ngOnDestroy(): void { 
    this.videoDetailDialogService.clearDialogTimer();
    this.profileDialogService.clearDialogTimer();
  }

  openDetailDialog(id: string): void{

    if(id === null || id === ''){
      console.error('video-summary-component', 'openDetailDialog', 'is is null or empty');
      return;
    }

    if(this.authService.isLoggedIn){
      this.videoDetailDialogService.openDialog(id);
    }
    else{
      this.redirectService.redirectToLogin();
    }
  }

  openProfileDialog(id: string): void{
    if(this.authService.isLoggedIn){
      this.profileDialogService.openDialog(id);
    }
    else{
      this.redirectService.redirectToLogin();
    }
  }

  onMouseOver(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = true;
    this.videoDetailDialogService.startDialogTimer(this.video.uniqueId);
  }

  onMouseOut(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = false;
    this.videoDetailDialogService.clearDialogTimer();
  }

  onMouseOverProfile(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = true;
    this.profileDialogService.startDialogTimer(this.video.creationUserUniqueId);
  }

  onMouseOutProfile(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = false;
    this.profileDialogService.clearDialogTimer();
  }
}
