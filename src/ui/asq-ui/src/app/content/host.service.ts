import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { StorageService } from '../_core/_services/storage.service';
import { HostSummaryDto } from '../_models/domain/host-summary-dto';
import { HostFilterConfigDto } from '../_models/domain/host-filter-config-dto';
import { HostQueryDto } from '../_models/domain/host-query-dto';
import { UserDto } from '../_models/domain/user-dto';

@Injectable({
  providedIn: 'root'
})
export class HostService {

  private baseRoute = 'host/';

  constructor(
    private http: HttpClient,
    private storage: StorageService
  ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  public get(id: string): Observable<UserDto>{
    const url = `${this._baseUrl}${this.baseRoute}${id}`;
    console.log(url);
    return this.http.get<UserDto>(url);
  }

  public buildSummaryQueries(): Observable<HostQueryDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}summary/queries`;
    console.log(url);
    const summaries = this.storage.hostSummaryQueries();
    console.log('host.service', 'buildSummaryQueries', 'summaries', summaries);
    if(summaries === null || summaries.length === 0){
      const obs = this.http.get<HostQueryDto[]>(url);
      obs.subscribe(resp => {
        this.storage.hostSummaryQueries(resp);
      });
      return obs;
    }
    else{
      return of(summaries);
    }
  }

  public getFilteredSummaries(filter: HostFilterConfigDto): Observable<HostSummaryDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}summary/filtered`;
    console.log(url);
    return this.http.post<HostSummaryDto[]>(url, filter);
  }
}
