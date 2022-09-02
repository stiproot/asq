import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { interval, Observable } from 'rxjs';
import { AuthService } from '@app/_core/_services/auth.service';
import { NotificationDialogService } from '@app/_core/_services/notification-dialog.service';
import { NotificationDto } from '@app/_models/domain/notification-dto';
import { NotificationQueryDto } from '@app/_models/domain/notification-query-dto';
import { NotificationService } from '../notification.service';

@Component({
  selector: 'app-notification-root',
  templateUrl: './notification-root.component.html',
  styleUrls: ['./notification-root.component.css']
})
export class NotificationRootComponent implements OnInit {

  notifications: NotificationDto[] = [];

  private notificationInterval: Observable<number>;
  private intervalCount: number = 1000 * 60 * 2;

  constructor(
    public authService: AuthService,
    private notificationService: NotificationService,
    private notificationDialogService: NotificationDialogService
  ) { }

  ngOnInit(): void { 
    this.loadMoreNotifications();

    //this.notificationInterval = interval(this.intervalCount);
    //this.notificationInterval.subscribe(n => {
      //if(this.authService.isLoggedIn){
        //this.loadMoreNotifications();
      //}
    //});
  }

  private loadMoreNotifications(): void{

    const olderThanId = this.notifications?.length ? this.notifications[this.notifications.length - 1].id : null;
    const query = new NotificationQueryDto();
    query.olderThanId = olderThanId;

    this.notificationService.getMore(query)
      .subscribe(resp => {
        if(this.notifications === undefined){
          this.notifications = resp;
        }
        else{
          this.notifications = this.notifications.concat(resp)
        }
      });
  }

  onSeenClicked($event: NotificationDto): void{
    console.log('notification-root', 'onSeenClicked', $event);

    const index = this.notifications.indexOf($event);
    console.log('index', index);

    this.notificationService.read($event.uniqueId)
      .subscribe(resp => {
        this.notifications.splice(index, 1);
        this.loadMoreNotifications();
      }, error => {
        this.notificationDialogService.showDialogMessage({
          contentText: 'Failed to read notification' + (<HttpErrorResponse>error).error,
          affirmBtnText: null,
          cancelBtnText: 'Ok',
          titleText: 'Error',
          additionalText: null
        });
      });
  }

  onScroll(): void{
    console.log('notification-root', 'onScroll');
    this.loadMoreNotifications();
  }
}
