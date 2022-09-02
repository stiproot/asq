import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatModule } from '../shared/mat.module';
import { NotificationComponent } from './notification/notification.component';
import { NotificationListComponent } from './notification-list/notification-list.component';
import { NotificationRootComponent } from './notification-root/notification-root.component';
import { SharedComponentModule } from './../components/shared-component.module';

@NgModule({
  declarations: [
    NotificationRootComponent,
    NotificationListComponent,
    NotificationComponent
  ],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MatModule,
    SharedComponentModule
  ],
  exports: [
    NotificationRootComponent,
    NotificationListComponent,
    NotificationComponent
  ]
})
export class NotificationModule { }
