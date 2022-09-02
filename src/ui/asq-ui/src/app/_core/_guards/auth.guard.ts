import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './../_services/auth.service';
// import { UserDto } from '@models/domain/user-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private authService: AuthService){ }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

      // const user: UserDto = this.authenticationService.userValue;

      // if(user){

        // if(next.data.roles && next.data.roles.indexOf(user.AccountTypeEnu) === -1){
          // //this.router.navigate['/']
          // return false;
        // }

        // return true;
        const url: string  = state.url;

        return this.checkLogin(url);
      // }

      // not logged in so redirect to the login page with the return url
      // this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
      // return false;
  }

  checkLogin(url: string): true | UrlTree{

    //console.log('auth.guard hit');

    if (this.authService.isLoggedIn){
      //console.log('is logged in');
      return true;
    }

    //this.authService.redirectUrl = url;

    //console.log('url', url);

    return this.router.parseUrl('/login?continue=' + encodeURI(url));
  }
}
