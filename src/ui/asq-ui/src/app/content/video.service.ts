import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '@environments/environment';
import { VideoDto } from '../_models/domain/video-dto';
import { VidDto } from '../_models/domain/vid-dto';
import { StorageService } from './../_core/_services/storage.service';
import { VideoSummaryDto } from './../_models/domain/video-summary-dto';
import { VideoFilterConfigDto } from './../_models/domain/video-filter-config-dto';
import { VideoQueryDto } from './../_models/domain/video-query-dto';
import { VideoSummaryQueryBuilderConfigDto } from './../_models/domain/video-summary-query-builder-config-dto';

@Injectable({
  providedIn: 'root'
})
export class VideoService {

  private baseRoute = 'video/';

  constructor(
    private http: HttpClient,
    private storage: StorageService
  ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  public get(guid: string): Observable<VideoDto>{
    const url = `${this._baseUrl}${this.baseRoute}${guid}`;
    console.log(url);
    return this.http.get<VideoDto>(url);
  }

  public post(video: VideoDto): Observable<VideoDto>{
    const url = `${this._baseUrl}${this.baseRoute}create`;
    console.log(url);
    return this.http.post<VideoDto>(url, video);
  }

  public put(video: VideoDto): Observable<HttpResponse<any>>{
    const url = `${this._baseUrl}${this.baseRoute}update`;
    console.log(url);
    return this.http.put<HttpResponse<any>>(url, video);
  }

  public buildSummaryQueries(config: VideoSummaryQueryBuilderConfigDto): Observable<VideoQueryDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}summary/queries`;
    console.log(url);
    const summaries = this.storage.videoSummaryQueries(config);
    console.log('summaries returned from storage', summaries);
    if(summaries === null || summaries.length === 0){
      const obs = this.http.post<VideoQueryDto[]>(url, config);
      obs.subscribe(resp => {
        this.storage.videoSummaryQueries(config, resp);
      });
      return obs;
    }
    else{
      return of(summaries);
    }
  }

  public getFilteredSummaries(filter: VideoFilterConfigDto): Observable<VideoSummaryDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}summary/filtered`;
    console.log(url);
    return this.http.post<VideoSummaryDto[]>(url, filter);
  }

  public upload(formData: FormData): Observable<VidDto>{
    const url = `${this._baseUrl}${this.baseRoute}upload`;
    console.log(url);
    return this.http.post<VidDto>(url, formData)
  }
}
