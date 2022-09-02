import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { OwlOptions, CarouselComponent } from 'ngx-owl-carousel-o';
import { ProfileDialogService } from '@app/_core/_services/dialog/profile-dialog.service';
import { BlogPostDialogService } from '@app/_core/_services/dialog/blog-post-dialog.service';
import { BlogPostSummaryDto } from '@app/_models/domain/blog-post-summary-dto';
import { AuthService } from '@app/_core/_services/auth.service';
import { RedirectService } from '@app/_core/_services/redirect.service';
import { OwlConfig } from '@app/_core/_config/owl-config';

@Component({
  selector: 'app-blog-panel',
  templateUrl: './blog-panel.component.html',
  styleUrls: [
    './blog-panel.component.css',
    './../../shared/shared.content.style.css'
  ],
  animations: [ ],
})
export class BlogPanelComponent implements OnInit {

  @Input() posts: BlogPostSummaryDto[];
  @ViewChild('owlC') c: CarouselComponent;

   options: OwlOptions;

  constructor(
    private authService: AuthService,
    private redirect: RedirectService,
    private profileDialogService: ProfileDialogService,
    private blogPostDialogService: BlogPostDialogService,
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
