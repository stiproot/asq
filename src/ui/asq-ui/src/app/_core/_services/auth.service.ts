import { Injectable } from '@angular/core';
import { Router } from "@angular/router";
import { BehaviorSubject, Observable, of } from "rxjs";
import { StorageService } from './storage.service';
import { RedirectService } from './redirect.service';
import { UserService } from './user.service';
import { UserDto } from './../../_models/domain/user-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userSubj: BehaviorSubject<UserDto>;
  public user: Observable<UserDto>;

  constructor(
    private router: Router,
    private storageService: StorageService,
    private userService: UserService,
    private redirectService: RedirectService
    ){
      this.userSubj = new BehaviorSubject<UserDto>(this.storageService.user());
      this.user = this.userSubj.asObservable();
    }

    public get userValue(): UserDto{
      return this.userSubj?.value;
    }

    public get isLoggedIn(): boolean{
      return this.userValue !== null;
    }

    public get isHost(): boolean{
      //console.log('authService', 'isHost', 'userValue', this.userValue);
      return this.userValue !== null && this.userValue?.host !== null;
    }
    
    public setUser(user: UserDto){
      this.userSubj.next(user);
    }

    login(username: string, password: string, errorCallback: CallableFunction = null): void{
      this.userService.login(username, password)
        .subscribe(resp => {
          this.storageService.user(resp);
          this.userSubj.next(resp);
          // check for redirect queryparam
          this.redirectService.resolveContinue();
        }, error => {
          if(errorCallback){
            errorCallback(error);
          }
        });
    }

    logout(): void{
      this.storageService.clearUserData();
      this.userSubj.next(null);
      this.router.navigate(['/videos']);
    }

    public clearUserData(): void{
      this.storageService.clearUserData();
    }
}
