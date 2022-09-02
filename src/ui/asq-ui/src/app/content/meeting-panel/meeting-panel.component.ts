import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { OwlOptions, CarouselComponent } from 'ngx-owl-carousel-o';
import { MeetingSummaryDto } from '@app/_models/domain/meeting-summary-dto';
import { OwlConfig } from '@app/_core/_config/owl-config';

@Component({
  selector: 'app-meeting-panel',
  templateUrl: './meeting-panel.component.html',
  styleUrls: [
    './meeting-panel.component.css', 
    './../../shared/shared.content.style.css'
  ],
  animations: [ ],
})
export class MeetingPanelComponent implements OnInit {

  @Input() meetings: MeetingSummaryDto[];
  @ViewChild('owlC') c: CarouselComponent;

  options: OwlOptions;

  constructor(
    private owlConfig: OwlConfig
  ) { 
    this.options = this.owlConfig.OWL_OPTIONS;
  }

  ngOnInit(): void { }

  next(): void{
    this.c.next();
  }

  prev(): void{
    this.c.prev();
  }
}