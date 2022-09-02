import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProgressBarService } from '@app/_core/_services/progress-bar.service';
import { BlogPostFilterConfigDto } from '@app/_models/domain/blog-post-filter-config-dto';
import { BlogPostSummaryDto } from '@app/_models/domain/blog-post-summary-dto';
import { ContentFilterConfigDto } from '@app/_models/domain/content-filter-config-dto';
import { BlogPostService } from '../blog-post.service';

@Component({
  selector: 'app-blog-search-root',
  templateUrl: './blog-search-root.component.html',
  styleUrls: ['./blog-search-root.component.css']
})
export class BlogSearchRootComponent implements OnInit {

  timer: number;
  blogPosts: BlogPostSummaryDto[] = [];
  filter: BlogPostFilterConfigDto = null;

  constructor(
    private progressBarService: ProgressBarService,
    private activatedRoute: ActivatedRoute,
    private blogPostService: BlogPostService
  ) 
  { 
    this.filter = new BlogPostFilterConfigDto();
    this.filter.take = 25;
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(q => {
      const query = q['q'];
      this.filter.elastic = query;
      this.clearTimer();
      if(query && query.length >= 3){
        this.setTimer();
      }
    });
  }

  private setTimer(): void{
    this.progressBarService.showProgressBar();
    this.timer = setTimeout(() => {
      this.getFiltered();
    }, 2200);
  }

  private getFiltered(): void{
    this.blogPostService.getFilteredSummaries(this.filter)
      .subscribe(resp => {
        console.log('meeting-search-root', 'getFiltered', 'resp', resp);
        this.blogPosts = resp;
        this.progressBarService.hideProgressBarDelayed();
      });
  }

  private clearTimer(): void{
    clearTimeout(this.timer);
    this.progressBarService.hideProgressBar();
  }

  onSearchCriteriaFilterClick(event: ContentFilterConfigDto): void{
    this.progressBarService.showProgressBar();
    this.filter = [event].map(x => 
      {
        const blogPostFilter = new BlogPostFilterConfigDto();
        blogPostFilter.elastic = x.elastic;
        blogPostFilter.foci = x.foci;
        blogPostFilter.creationUserName = x.name;
        blogPostFilter.creationUserUniqueId = x.creationUserUniqueId;
        blogPostFilter.title = x.title;
        blogPostFilter.description = x.description;
        return blogPostFilter;
      })[0];
    
    this.clearTimer();
    this.getFiltered();
  }

}
