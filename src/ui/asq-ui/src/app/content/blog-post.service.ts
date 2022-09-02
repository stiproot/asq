import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '@environments/environment';
import { BlogPostDto } from '../_models/domain/blog-post-dto';
import { BlogPostQueryDto } from '../_models/domain/blog-post-query-dto';
import { BlogPostFilterConfigDto } from '../_models/domain/blog-post-filter-config-dto';
import { BlogPostSummaryDto } from '../_models/domain/blog-post-summary-dto';
import { StorageService } from '@app/_core/_services/storage.service';
import { BlogSummaryQueryBuilderConfigDto } from '@app/_models/domain/blog-summary-query-builder-config-dto';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  private baseRoute = 'blog/';

  constructor(
    private http: HttpClient,
    private storage: StorageService
  ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  public get(id: string): Observable<BlogPostDto>{
    const url = `${this._baseUrl}${this.baseRoute}${id}`;
    console.log(url);
    return this.http.get<BlogPostDto>(url);
  }

  public post(blogPost: BlogPostDto): Observable<BlogPostDto>{
    const url = `${this._baseUrl}${this.baseRoute}create`;
    console.log(url);
    return this.http.post<BlogPostDto>(url, blogPost);
  }

  public put(blogPost: BlogPostDto): Observable<HttpResponse<any>>{
    const url = `${this._baseUrl}${this.baseRoute}update`;
    console.log(url);
    return this.http.put<HttpResponse<any>>(url, blogPost);
  }

  public buildSummaryQueries(config: BlogSummaryQueryBuilderConfigDto): Observable<BlogPostQueryDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}summary/queries`;
    console.log(url);
    const summaries = this.storage.blogPostSummaryQueries(config);
    console.log('blog-post.service', 'buildSUmmaryQueries', 'summaries', summaries);
    if(summaries === null || summaries.length === 0){
      const obs = this.http.post<BlogPostQueryDto[]>(url, config);
      obs.subscribe(resp => {
        this.storage.blogPostSummaryQueries(config, resp);
      });
      return obs;
    }
    else{
      return of(summaries);
    }
  }

  public getFilteredSummaries(filter: BlogPostFilterConfigDto): Observable<BlogPostSummaryDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}summary/filtered`;
    console.log(url);
    return this.http.post<BlogPostSummaryDto[]>(url, filter);
  }
}
