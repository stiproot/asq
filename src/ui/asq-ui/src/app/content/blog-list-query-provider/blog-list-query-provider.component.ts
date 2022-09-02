import { Component, OnInit, Input } from '@angular/core';
import { BlogPostQueryDto } from '@app/_models/domain/blog-post-query-dto';
import { BlogPostService } from '../blog-post.service';
import { BlogSummaryQueryBuilderConfigDto } from '@app/_models/domain/blog-summary-query-builder-config-dto';

@Component({
  selector: 'app-blog-list-query-provider',
  templateUrl: './blog-list-query-provider.component.html',
  styleUrls: ['./blog-list-query-provider.component.css']
})
export class BlogListQueryProviderComponent implements OnInit {

  @Input() inputCreationUserUniqueId: string | null = null;

  //models
  queries: BlogPostQueryDto[] = null;

  private config: BlogSummaryQueryBuilderConfigDto = new BlogSummaryQueryBuilderConfigDto();

  private get creationUserUniqueId(): string | null{
    if(this.inputCreationUserUniqueId){
      return this.inputCreationUserUniqueId;
    }
    return null;
  }

  constructor(
    private blogPostService: BlogPostService
  ) { }

  ngOnInit(): void {
    this.config.creationUserUniqueId = this.creationUserUniqueId;
    this.buildQueies();
  }

  private buildQueies(): void{
    this.blogPostService.buildSummaryQueries(this.config)
      .subscribe(resp => {
        console.log('blog-list-query-provider', 'buildQueries', 'resp', resp);
        this.queries = resp;
      });
  }
}
