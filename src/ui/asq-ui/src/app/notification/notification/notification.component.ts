import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NotificationDto } from '@app/_models/domain/notification-dto';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {

  @Input() notification: NotificationDto;
  @Output() seenClicked = new EventEmitter<any>();

  constructor() { }

  ngOnInit(): void { }

  onSeenClick($event: Event): void{
    console.log('notification', 'onSeenClick', 'notification', this.notification);

    this.seenClicked.emit(this.notification);
  }
}
