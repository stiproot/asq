import { Component, OnInit, Inject, Input, Optional } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserDto } from '@app/_models/domain/user-dto';
import { RegexConstants } from '@app/_core/_constants/regex-constants';
import { FocusDto } from '@app/_models/domain/focus-dto';
import { UserService } from '@app/_core/_services/user.service';

@Component({
  selector: 'app-profile-detail',
  templateUrl: './profile-detail.component.html',
  styleUrls: ['./profile-detail.component.css']
})
export class ProfileDetailComponent implements OnInit {

  user: UserDto;
  public interests: FocusDto[] = null; 
  public specialities: FocusDto[] = null; 

  // inputs
  @Input() inputId: string | null;

  public get id(): string | null{
    // query param
    if(this.router.url.match(this.regex.PROFILE_URL_REGEX)){
      return this.route.snapshot.paramMap.get('id');
    }
    // dialog
    else if(this.injectedId){
      return this.injectedId;
    }
    // component
    else if(this.inputId){
      return this.inputId;
    }
    else{
      throw Error('No profile id provided');
    }
  }

  get isDialog(): boolean{
    // this is actually not going to work... as there could be a dialog open when the url matches the profile url regex
    //return this.router.url.match(this.regex.PROFILE_URL_REGEX) === null;
    return this.injectedId !== null; 
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private regex: RegexConstants,
    private userService: UserService,
    @Optional() @Inject(MAT_DIALOG_DATA) private injectedId: string
  ) { 
    console.log('injectedId received in profile', this.injectedId);
  }

  ngOnInit(): void {
    this.initModel();
  }

  private mapFoci(): void{
    this.interests = this.user.interests.map(v => v.focus);
    this.specialities = this.user.host.specialities.map(v => v.focus);
  }

  private initModel(): void{

    console.log('profile-detail', 'initModel', 'id', this.id);

    this.userService.get(this.id)
      .subscribe(resp => {
        console.log('profile-detail', 'initModel', 'user', resp);
        this.user = resp;
        this.mapFoci();
      });
  }
}
