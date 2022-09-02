import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DOCUMENT } from '@angular/common';
import { ZoomMtg } from '@zoomus/websdk';
import { environment } from '@environments/environment';
import { MeetingService } from '@app/content/meeting.service';
import { MeetingDto } from '@app/_models/domain/meeting-dto';
import { JoinMeetingConfigDto } from '@app/_models/domain/join-meeting-config-dto';
import { AuthService } from '@app/_core/_services/auth.service';
import { NotificationDialogService } from '@app/_core/_services/notification-dialog.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ProgressBarService } from '@app/_core/_services/progress-bar.service';

@Component({
  selector: 'app-meeting-gateway',
  templateUrl: './meeting-gateway.component.html',
  styleUrls: ['./meeting-gateway.component.css']
})
export class MeetingGatewayComponent implements OnInit {

  meeting: MeetingDto;
  meetingConfig: JoinMeetingConfigDto;

  private get meetingId(): string{
    return this.route.snapshot.paramMap.get('id');
  }

  private get username(): string{
    const user = this.authservice.userValue;
    return `${user.name} '${user.username}' ${user.surname}`;
  }

  private get leaveUrl(): string{
    return `${environment.domain}meeting/${this.meetingId}`;
  }

  constructor(
    private authservice: AuthService,
    private meetingService: MeetingService,
    private notification: NotificationDialogService,
    private progressBarService: ProgressBarService,
    private route: ActivatedRoute,
    @Inject(DOCUMENT) document) { }

  ngOnInit(): void {

    ZoomMtg.preLoadWasm();
    ZoomMtg.prepareJssdk();

    this.progressBarService.showProgressBar();

    this.meetingService.authorize(this.meetingId)
      .subscribe((data: any) => {
        console.log('data', data);
        this.meeting = data.meeting;
        this.meetingConfig = data.meetingConfig;
        this.startMeeting();
      }, error => {
          this.notification.showDialogMessage({
            contentText: 'Unable to authorize meeting entry - ' + (<HttpErrorResponse>error).error,
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Error',
            additionalText: null
          });
          this.progressBarService.hideProgressBarDelayed();
      });
  }

  startMeeting(): void{
    console.log('attempting meeting start');

    document.getElementById('zmmtg-root').style.display = 'block';

    ZoomMtg.init({
      leaveUrl: this.leaveUrl, 
      isSupportAV: true,
      success: (success) => {
        console.log('Init meeting successful', success);
        console.log(this.meetingConfig);
        ZoomMtg.join({
          signature: this.meetingConfig.signature,
          meetingNumber: this.meetingConfig.meetingNumber,
          userName: this.username,
          apiKey: this.meetingConfig.apiKey,
          userEmail: this.meetingConfig.userEmail,
          passWord: this.meetingConfig.passWord,
          success: (success) => {
            console.log('Join meeting successful', success);
          },
          error: (error) => {
            this.notification.showDialogMessage({
              contentText: 'Unable to join meeting - ' + (<HttpErrorResponse>error).error,
              affirmBtnText: null,
              cancelBtnText: 'Ok',
              titleText: 'Error',
              additionalText: null
            });
            this.progressBarService.hideProgressBarDelayed();
          }
        });
      },
      error: (error) => {
        this.notification.showDialogMessage({
          contentText: 'Unable to initiate meeting - ' + (<HttpErrorResponse>error).error,
          affirmBtnText: null,
          cancelBtnText: 'Ok',
          titleText: 'Error',
          additionalText: null
        });
        this.progressBarService.hideProgressBarDelayed();
      }
    });
  }

  //onStartMeetingClick(event: Event): void{
    //this.startMeeting();
  //}
}
