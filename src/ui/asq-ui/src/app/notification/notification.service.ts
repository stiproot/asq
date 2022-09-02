import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '@environments/environment';
import { StorageService } from '../_core/_services/storage.service';
import { NotificationDto } from '../_models/domain/notification-dto';
import { NotificationQueryDto } from '../_models/domain/notification-query-dto';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private baseRoute = 'notification/';

  constructor(
    private http: HttpClient,
    private storage: StorageService
  ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  public get(id: string): Observable<NotificationDto>{
    const url = `${this._baseUrl}${this.baseRoute}${id}`;
    console.log(url);
    return this.http.get<NotificationDto>(url);
  }

  public getMore(query: NotificationQueryDto): Observable<NotificationDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}more`;
    console.log(url);
    return this.http.post<NotificationDto[]>(url, query);
  }

  public read(id: string): Observable<boolean>{
    const url = `${this._baseUrl}${this.baseRoute}${id}/read`;
    console.log(url);
    return this.http.get<boolean>(url);
  }
}
