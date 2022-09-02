import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@app/_core/_services/auth.service';
import { MeetingDto } from '@app/_models/domain/meeting-dto';

@Component({
  selector: 'app-start-meeting-button',
  templateUrl: './start-meeting-button.component.html',
  styleUrls: ['./start-meeting-button.component.css']
})
export class StartMeetingButtonComponent implements OnInit {

  @Input() meeting: MeetingDto;

  private get meetingIsNullOrUnderfined(): boolean{
    return this.meeting === undefined || this.meeting === null;
  }

  private get hasUserIsRegisteredForMeeting(): boolean{

    if(this.meetingIsNullOrUnderfined || this.meeting.participants === null || this.meeting?.participants.length === 0){
      return false;
    }

    var participant = this.meeting.participants.filter(p => p.userId === this.authService.userValue.id && !p.inactive);
    return participant.length > 0;
  }

  private get isUserHostOfMeeting(): boolean{
    if(this.meetingIsNullOrUnderfined){
      return false;
    }

    return this.authService.isHost && this.authService.userValue.host.id === this.meeting.hostId;
  }

  private get isMeetingInConstruction(): boolean{
    return !this.meetingIsNullOrUnderfined && this.meeting.id <= 0;
  }

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  get isButtonVisible(): boolean{
    return !this.meeting.hasPassed && 
      (this.hasUserIsRegisteredForMeeting || (!this.isMeetingInConstruction && this.isUserHostOfMeeting));
  }

  get buttonText(): string{
    return this.isUserHostOfMeeting ? 'Start Meeting' : 'Join Meeting';
  }

  get zoomButtonText(): string{
    return this.isUserHostOfMeeting ? 'Start Meeting In Zoom' : 'Join Meeting In Zoom';
  }

  ngOnInit(): void { }

  onButtonClick(event: Event): void{
    this.router.navigate(['/meeting-gateway/' + this.meeting.uniqueId]);
  }

  onZoomButtonClick(event: Event): void{
    const extMeeting = JSON.parse(this.meeting.extMeeting.payload);
    const url = this.isUserHostOfMeeting ? extMeeting.start_url : extMeeting.join_url;
    window.open(url, '_blank');
  }
}
