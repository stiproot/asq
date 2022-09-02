import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '@environments/environment';
import { MeetingDto } from '../_models/domain/meeting-dto';
import { DateTimeContainerDto } from '@app/_models/temp/datetime-container-dto';

@Injectable({
  providedIn: 'root'
})
export class DevService {

  private baseRoute = 'dev/';

  constructor(
    private http: HttpClient
  ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  public post(dateTimeContainer: DateTimeContainerDto): Observable<HttpResponse<DateTimeContainerDto>>{
    const url = `${this._baseUrl}${this.baseRoute}log-datetime`;
    console.log(url);
    return this.http.post<HttpResponse<DateTimeContainerDto>>(url, dateTimeContainer);
  }
}
