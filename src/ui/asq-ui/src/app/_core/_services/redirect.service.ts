import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RedirectService {

  constructor(
    private router: Router, 
    private route: ActivatedRoute,
  ) { }

  private extractQueryParam(key: string): string{
    return this.route.snapshot.queryParamMap.get(key);
  }

  public resolveContinue(): void{
    const url: string = this.extractQueryParam('continue');
    if (url){
      this.router.navigate([url]);
    }
  }

  public redirectToLogin(): void{
    this.router.navigate(['/login'], { queryParams: { continue: this.router.parseUrl(this.router.url) }});
  }
}
