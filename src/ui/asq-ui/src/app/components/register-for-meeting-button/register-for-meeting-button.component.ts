import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import * as moment from 'moment';
import { MeetingService } from '@app/content/meeting.service';
import { NotificationDialogService } from '@app/_core/_services/notification-dialog.service';
import { MeetingDto } from '@app/_models/domain/meeting-dto';
import { AuthService } from '@app/_core/_services/auth.service';
import { MeetingStatusEnum } from '@app/_models/domain/meeting-status-enum';

@Component({
  selector: 'app-register-for-meeting-button',
  templateUrl: './register-for-meeting-button.component.html',
  styleUrls: ['./register-for-meeting-button.component.css']
})
export class RegisterForMeetingButtonComponent implements OnInit {

  @Input() meeting: MeetingDto;
  @Output() registrationSuccessful = new EventEmitter<Event>();
  @Output() deregistrationSuccessful = new EventEmitter<Event>();
  @Output() registrationFailure = new EventEmitter<Event>();

  get isRegisteredForMeeting(): boolean{
    //console.log('register-for-meeting-button', 'meeting', this.meeting);
    var participant = this.meeting.participants.filter(p => p.userId === this.authService.userValue.id && !p.inactive);
    return participant.length > 0;
  }

  get buttonText(): string{
    return this.isRegisteredForMeeting ? 'De-register': 'Register';
  }

  get isButtonVisible(): boolean{

    const isModelNotNull = this.meeting !== null;
    const isMeetingStatusValid = this.meeting.meetingStatusId === MeetingStatusEnum.AWAITING;
    const isMeetingCreatorNotActiveUser = this.meeting.creationUserId !== this.authService.userValue.id;
    const isMeetingStartDateInTheFuture = !this.meeting.hasPassed; //moment.utc().diff(moment(this.meeting.startDateUtc), 'seconds') < 0;
    const visible = isModelNotNull && isMeetingStatusValid && isMeetingCreatorNotActiveUser && isMeetingStartDateInTheFuture;

    //console.log('meeting-detail', 'isMeetingStatusValid', isMeetingStatusValid);
    //console.log('meeting-detail', 'isMeetingStartDateInTheFuture', isMeetingStartDateInTheFuture);
    //console.log('meeting-detail', 'isMeetingCreatorActiveUser', isMeetingCreatorNotActiveUser);
    //console.log('meeting-detail', 'valid', valid);

    return visible;
  }

  constructor(
    private meetingService: MeetingService,
    private notification: NotificationDialogService,
    private authService: AuthService
  ) { }

  ngOnInit(): void { }

  onRegisterClick(event: Event): void{

    const registering = !this.isRegisteredForMeeting;
    const successContentText = `Meeting ${(registering ? 'registration' : 'de-registration')} successful`;
    const failureContentText = `Meeting ${(registering ? 'registration' : 'de-registration')} failed`;

    this.meetingService.register(this.meeting.uniqueId, registering)
      .subscribe(data => {
        console.log(data);
        this.notification.showDialogMessage({
          contentText: successContentText,
          affirmBtnText: null,
          cancelBtnText: 'Ok',
          titleText: 'Success',
          additionalText: null
        });
        if(registering){
          this.registrationSuccessful.emit();
        }
        else{
          this.deregistrationSuccessful.emit();
        }
      }, error => {
        this.notification.showDialogMessage({
          contentText: failureContentText + ' - ' + (<HttpErrorResponse>error).error,
          affirmBtnText: null,
          cancelBtnText: 'Ok',
          titleText: 'Error',
          additionalText: null
        });
        this.registrationFailure.emit();
      });
  }
}
