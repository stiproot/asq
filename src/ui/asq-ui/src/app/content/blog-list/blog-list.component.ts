import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { BlogPostService } from './../blog-post.service';
import { BlogPostQueryDto } from '@app/_models/domain/blog-post-query-dto';

@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.css']
})
export class BlogListComponent implements OnInit {

  @Input() queries: BlogPostQueryDto[];

  constructor(
    private router: Router,
    private blogPostService: BlogPostService
  ) { }

  ngOnInit(): void {
    this.processQueries();
  }

  processQueries(): void{
    this.queries.forEach((v, i) => {
      this.blogPostService.getFilteredSummaries(v.config)
        .subscribe(resp => {
          v.id = 'caro_' + i;
          v.data = resp;
        });
    });
  }

  onNewBlogPostClick(event: Event): void{
    this.router.navigate(['/blog-post/new'], { queryParamsHandling: 'merge' });
  }
}
