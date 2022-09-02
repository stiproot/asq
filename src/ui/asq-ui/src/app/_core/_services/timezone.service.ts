import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { TimezoneDto } from './../../_models/domain/timezone-dto';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class TimezoneService {

  private baseRoute = 'timezone/';

  constructor(
    private http: HttpClient,
    private storage: StorageService
    ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  private store(o: Observable<TimezoneDto[]>): void{
    o.subscribe(data => {
      console.log('writing timezones to storage');
      this.storage.timezones(data);
    });
  }

  public getAll(): Observable<TimezoneDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}all`;
    console.log(url);
    const timezones = this.storage.timezones();
    if (timezones == null || timezones.length === 0){
      const obs = this.http.get<TimezoneDto[]>(url);
      this.store(obs);
      return obs;
    }
    else{
      console.log('found timezones in storage');
      return of(timezones);
    }
  }
}

