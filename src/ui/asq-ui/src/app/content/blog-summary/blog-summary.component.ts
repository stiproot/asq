import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { AuthService } from '@app/_core/_services/auth.service';
import { BlogPostDialogService } from '@app/_core/_services/dialog/blog-post-dialog.service';
import { ProfileDialogService } from '@app/_core/_services/dialog/profile-dialog.service';
import { RedirectService } from '@app/_core/_services/redirect.service';
import { BlogPostSummaryDto } from '@app/_models/domain/blog-post-summary-dto';

@Component({
  selector: 'app-blog-summary',
  templateUrl: './blog-summary.component.html',
  styleUrls: [
    './blog-summary.component.css',
    './../../shared/shared.content.style.css'
    ]
})
export class BlogSummaryComponent implements OnInit, OnDestroy {

  @Input() blog: BlogPostSummaryDto = null;
  showProgressBar: boolean = false;

  constructor(

    private authService: AuthService,
    private redirectService: RedirectService,
    private profileDialogService: ProfileDialogService,
    private blogDialogService: BlogPostDialogService,
  ) { }

  ngOnInit(): void { }

  ngOnDestroy(): void { 
    this.blogDialogService.clearDialogTimer();
    this.profileDialogService.clearDialogTimer();
  }

  openDetailDialog(id: string): void{

    if(id === null || id === ''){
      console.error('meeting-summary-component', 'openLectureDetailDialog', 'is is null or empty');
      return;
    }

    if(this.authService.isLoggedIn){
      this.blogDialogService.openDialog(id);
    }
    else{
      this.redirectService.redirectToLogin();
    }
  }

  openProfileDialog(id: string): void{
    if(this.authService.isLoggedIn){
      this.profileDialogService.openDialog(id);
    }
    else{
      this.redirectService.redirectToLogin();
    }
  }

  onMouseOver(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = true;
    this.blogDialogService.startDialogTimer(this.blog.uniqueId);
  }

  onMouseOut(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = false;
    this.blogDialogService.clearDialogTimer();
  }

  onMouseOverProfile(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = true;
    this.profileDialogService.startDialogTimer(this.blog.creationUserUniqueId);
  }

  onMouseOutProfile(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = false;
    this.profileDialogService.clearDialogTimer();
  }
}
