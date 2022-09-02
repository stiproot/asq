import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { OwlOptions, CarouselComponent } from 'ngx-owl-carousel-o';
import { ProfileDialogService } from '@app/_core/_services/dialog/profile-dialog.service';
import { HostSummaryDto } from '@app/_models/domain/host-summary-dto';
import { AuthService } from '@app/_core/_services/auth.service';
import { RedirectService } from '@app/_core/_services/redirect.service';
import { OwlConfig } from '@app/_core/_config/owl-config';

@Component({
  selector: 'app-profile-panel',
  templateUrl: './profile-panel.component.html',
  styleUrls: [
    './profile-panel.component.css',
    './../../shared/shared.content.style.css'
  ]
})
export class ProfilePanelComponent implements OnInit {

  @Input() hosts: HostSummaryDto[];
  @ViewChild('owlC') c: CarouselComponent;

   options: OwlOptions;

  constructor(
    private authService: AuthService,
    private redirect: RedirectService,
    private profileDialogService: ProfileDialogService,
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

  openDetailDialog(id: number): void{
    if(this.authService.isLoggedIn){
      this.profileDialogService.openDialog(id.toString());
    }
    else{
      this.redirect.redirectToLogin();
    }
  }
}
