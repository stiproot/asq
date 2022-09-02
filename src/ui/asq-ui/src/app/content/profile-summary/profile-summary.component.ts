import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '@app/_core/_services/auth.service';
import { ProfileDialogService } from '@app/_core/_services/dialog/profile-dialog.service';
import { RedirectService } from '@app/_core/_services/redirect.service';
import { HostSummaryDto } from '@app/_models/domain/host-summary-dto';

@Component({
  selector: 'app-profile-summary',
  templateUrl: './profile-summary.component.html',
  styleUrls: [
    './profile-summary.component.css',
    './../../shared/shared.content.style.css'
    ]
})
export class ProfileSummaryComponent implements OnInit, OnDestroy {

  @Input() host: HostSummaryDto = null;
  showProgressBar: boolean = false;

  constructor(
    private authService: AuthService,
    private redirectService: RedirectService,
    private profileDialogService: ProfileDialogService,
  ) { }

  ngOnInit(): void { }

  ngOnDestroy(): void { 
    this.profileDialogService.clearDialogTimer();
  }

  openDetailDialog(id: string): void{
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
    this.profileDialogService.startDialogTimer(this.host.uniqueId);
  }

  onMouseOut(): void{
    if(!this.authService.isLoggedIn) return;
    this.showProgressBar = false;
    this.profileDialogService.clearDialogTimer();
  }
}
