import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatModule } from './../shared/mat.module';
import { FocusListComponent } from './focus-list/focus-list.component';
import { ImgSelectComponent } from './img-select/img-select.component';
import { PersistFabComponent } from './persist-fab/persist-fab.component';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { MessageDialogComponent } from './message-dialog/message-dialog.component';
import { TimezoneSelectComponent } from './timezone-select/timezone-select.component';
import { BackButtonComponent } from './back-button/back-button.component';
import { RegisterForMeetingButtonComponent } from './register-for-meeting-button/register-for-meeting-button.component';
import { InfiniteScrollComponent } from './infinite-scroll/infinite-scroll.component';
import { StartMeetingButtonComponent } from './start-meeting-button/start-meeting-button.component';
import { VideoSelectComponent } from './video-select/video-select.component';

@NgModule({
  declarations: [
    FocusListComponent,
    ImgSelectComponent,
    PersistFabComponent,
    ConfirmDialogComponent,
    MessageDialogComponent,
    TimezoneSelectComponent,
    BackButtonComponent,
    RegisterForMeetingButtonComponent,
    InfiniteScrollComponent,
    StartMeetingButtonComponent,
    VideoSelectComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatModule
  ],
  exports: [
    FocusListComponent,
    ImgSelectComponent,
    VideoSelectComponent,
    PersistFabComponent,
    MessageDialogComponent,
    ConfirmDialogComponent,
    TimezoneSelectComponent,
    BackButtonComponent,
    RegisterForMeetingButtonComponent,
    InfiniteScrollComponent,
    StartMeetingButtonComponent
  ]
})
export class SharedComponentModule { }
