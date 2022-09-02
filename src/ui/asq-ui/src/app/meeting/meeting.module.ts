import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ZoomMtg } from '@zoomus/websdk';
import { MeetingGatewayComponent } from './meeting-gateway/meeting-gateway.component';

@NgModule({
  declarations: [
    MeetingGatewayComponent
  ],
  imports: [
    CommonModule
  ]
  // exports: [
    // ZoomMtg
  // ]
})
export class MeetingModule { }
