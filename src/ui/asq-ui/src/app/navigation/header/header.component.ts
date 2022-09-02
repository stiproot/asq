import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from './../../_core/_services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProgressBarService } from '@app/_core/_services/progress-bar.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Output() public sidenavToggle = new EventEmitter();
  public searchString: string = null;
  private timer: number = null;

  private searchUrls = [
     '/video-search',
     '/meeting-search',
     '/blog-post-search',
     '/profile-search'
  ];

  private searchUrlMappings = {
    '/videos': '/video-search',
    '/video-search': '/videos',

    '/meetings': '/meeting-search',
    '/meeting-search': '/meetings',

    '/blog-posts': '/blog-post-search',
    '/blog-post-search': '/blog-posts',

    '/hosts': '/profile-search',
    '/profile-search': '/hosts'
  };

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public authService: AuthService,
    private progressBarService: ProgressBarService
  ) { }

  ngOnInit(): void { 
    this.activatedRoute.queryParams.subscribe(qp => {
      const q = qp['q'];
      console.log('header', 'q', q);
      if(q && q !== this.searchString){
        this.searchString = q;
      }
    })
  }

  public onToggleSidenav = () => {
    this.sidenavToggle.emit();
  }

  onProfileClick(event: Event): void{
    this.router.navigate(['/profile/' + this.authService.userValue.uniqueId]);
  }

  // search ------------------------------

  onSearchChange(): void{
    console.log('onSearchChange', 'searchString', this.searchString);
    clearTimeout(this.timer);
    this.timer = setTimeout(() => {
      this.navigateToSearchView();
    }, 1500);
  }

  onSearchClearClick(): void{
    clearTimeout(this.timer);
    this.searchString = '';
    var url = this.urlWithoutParams;
    url = this.searchUrlMappings[url];
    this.router.navigate([url]);
  }

  private get canSearchStringTriggerSearch(): boolean{
    return this.searchString !== null && this.searchString !== undefined && this.searchString.length >= 3;
  }

  private navigateToSearchView(): void{

    if(this.canSearchStringTriggerSearch){
      console.log('Search string can trigger search', 'search', this.searchString);

      var url = this.urlWithoutParams;
      if(!this.isCurrentlyOnSearchView){
        url = this.searchUrlMappings[url];
      }
      console.log('header', 'navigateToSearchView', 'search', this.searchString);
      this.router.navigate([url], { queryParams: { q: this.searchString }});
    }
    else if(this.searchString === null || this.searchString.length === 0){
        console.log('Search string is null of has length of 0', 'search', this.searchString);
        var url = this.urlWithoutParams;
        url = this.searchUrlMappings[url];
        this.router.navigate([url]);
    }
  }

  private get isCurrentlyOnSearchView(): boolean{
    return this.searchUrls.indexOf(this.urlWithoutParams) != -1;
  }

  private get urlWithoutParams(): string{
    return this.router.url.split('?')[0];
  }

  navigate(navigationStr: string): void{
    this.searchString = null;
    this.router.navigate([navigationStr]);
  }
}
