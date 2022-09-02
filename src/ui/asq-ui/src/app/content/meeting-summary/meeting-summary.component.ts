import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { AuthService } from '@app/_core/_services/auth.service';
import { MeetingDetailDialogService } from '@app/_core/_services/dialog/meeting-detail-dialog.service';
import { ProfileDialogService } from '@app/_core/_services/dialog/profile-dialog.service';
import { RedirectService } from '@app/_core/_services/redirect.service';
import { MeetingSummaryDto } from '@app/_models/domain/meeting-summary-dto';

@Component({
  selector: 'app-meeting-summary',
  templateUrl: './meeting-summary.component.html',
  styleUrls: [
    './meeting-summary.component.css',
    './../../shared/shared.content.style.css'
  ]
})
export class MeetingSummaryComponent implements OnInit, OnDestroy {

  @Input() meeting: MeetingSummaryDto = null;
  showProgressBar: boolean = false;

  constructor(
    private authService: AuthService,
    private redirectService: RedirectService,
    private profileDialogService: ProfileDialogService,
    private meetingDetailDialogService: MeetingDetailDialogService
  ) { }

  ngOnInit(): void { }

  ngOnDestroy(): void { 
    this.meetingDetailDialogService.clearDialogTimer();
    this.profileDialogService.clearDialogTimer();
  }

  openLectureDetailDialog(id: string): void{

    if(id === null || id === ''){
      console.error('meeting-summary-component', 'openLectureDetailDialog', 'is is null or empty');
      return;
    }

    if(this.authService.isLoggedIn){
      this.meetingDetailDialogService.openDialog(id);
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
    this.meetingDetailDialogService.startDialogTimer(this.meeting.uniqueId);
  }

  onMouseOut(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = false;
    this.meetingDetailDialogService.clearDialogTimer();
  }

  onMouseOverProfile(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = true;
    this.profileDialogService.startDialogTimer(this.meeting.creationUserUniqueId);
  }

  onMouseOutProfile(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = false;
    this.profileDialogService.clearDialogTimer();
  }
}
