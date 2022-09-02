import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { OwlOptions, CarouselComponent } from 'ngx-owl-carousel-o';
import { VideoSummaryDto } from '@app/_models/domain/video-summary-dto';
import { OwlConfig } from '@app/_core/_config/owl-config';

@Component({
  selector: 'app-video-panel',
  templateUrl: './video-panel.component.html',
  styleUrls: [
    './video-panel.component.css', 
    './../../shared/shared.content.style.css'
  ],
  animations: [ ],
})
export class VideoPanelComponent implements OnInit {

  @Input() videos: VideoSummaryDto[];
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