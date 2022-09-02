import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '@environments/environment';
import { MeetingDto } from '../_models/domain/meeting-dto';
import { StorageService } from '@app/_core/_services/storage.service';
import { MeetingSummaryDto } from '@app/_models/domain/meeting-summary-dto';
import { MeetingFilterConfigDto } from '@app/_models/domain/meeting-filter-config-dto';
import { MeetingQueryDto } from '@app/_models/domain/meeting-query-dto';
import { MeetingSummaryQueryBuilderConfigDto } from '@app/_models/domain/meeting-summary-query-builder-config-dto';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {

  private baseRoute = 'meeting/';

  constructor(
    private http: HttpClient,
    private storage: StorageService
  ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  public get(meetingId: string): Observable<MeetingDto>{
    const url = `${this._baseUrl}${this.baseRoute}${meetingId}`;
    console.log(url);
    return this.http.get<MeetingDto>(url);
  }

  public post(meeting: MeetingDto): Observable<MeetingDto>{
    const url = `${this._baseUrl}${this.baseRoute}create`;
    console.log(url);
    return this.http.post<MeetingDto>(url, meeting);
  }

  public put(meeting: MeetingDto): Observable<HttpResponse<any>>{
    const url = `${this._baseUrl}${this.baseRoute}update`;
    console.log(url);
    return this.http.put<HttpResponse<any>>(url, meeting);
  }

  public register(meetingId: string, register: boolean = true): Observable<boolean>{
    const url = `${this._baseUrl}${this.baseRoute}${meetingId}/register/${register}`;
    console.log(url);
    return this.http.get<boolean>(url);
  }

  public authorize(meetingId: string): Observable<HttpResponse<any>>{
    const url = `${this._baseUrl}ext${this.baseRoute}authorize/${meetingId}`;
    console.log(url);
    return this.http.get<any>(url);
  }

  public buildSummaryQueries(config: MeetingSummaryQueryBuilderConfigDto): Observable<MeetingQueryDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}summary/queries`;
    console.log(url);
    const summaries = this.storage.lectureSummaryQueries(config);
    console.log('summaries returned from storage', summaries);
    if(summaries === null || summaries.length === 0){
      const obs = this.http.post<MeetingQueryDto[]>(url, config);
      obs.subscribe(resp => {
        this.storage.lectureSummaryQueries(config, resp);
      });
      return obs;
    }
    else{
      return of(summaries);
    }
  }

  public getFilteredSummaries(filter: MeetingFilterConfigDto): Observable<MeetingSummaryDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}summary/filtered`;
    console.log(url);
    return this.http.post<MeetingSummaryDto[]>(url, filter);
  }
}
