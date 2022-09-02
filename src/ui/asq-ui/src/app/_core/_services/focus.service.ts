import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { FocusDto } from './../../_models/domain/focus-dto';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class FocusService {

  private baseRoute = 'focus/';

  constructor(
    private http: HttpClient,
    private storage: StorageService
    ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  private store(o: Observable<FocusDto[]>): void{
    o.subscribe(data => {
      console.log('writing foci to storage');
      this.storage.foci(data);
    });
  }

  public getAll(): Observable<FocusDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}all`;
    console.log(url);
    const foci = this.storage.foci();
    if (foci == null){
      const obs = this.http.get<FocusDto[]>(url);
      this.store(obs);
      return obs;
    }
    else{
      console.log('found foci in storage');
      return of(foci);
    }
  }
}

