import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { NotificationDto } from '@app/_models/domain/notification-dto';

@Component({
  selector: 'app-notification-list',
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.css']
})
export class NotificationListComponent implements OnInit, OnDestroy {

  @Input() notifications: NotificationDto[] = [];
  @Output() seenClicked = new EventEmitter<any>();
  @Output() scrolled = new EventEmitter<any>();

  isLoading: boolean = false;

  constructor() { }

  ngOnInit(): void { }

  ngOnDestroy(): void{ }

  onSeenClicked($event: any): void{
    console.log('notification-list', 'onSeenClicked', $event);
    this.seenClicked.emit($event);
  }

  onScroll(): void{
    console.log('notification-list', 'onScroll');
    this.scrolled.emit();
  }
}
