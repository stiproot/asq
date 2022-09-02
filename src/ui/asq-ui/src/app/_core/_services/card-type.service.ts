import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { CardTypeDto } from './../../_models/domain/card-type-dto';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class CardTypeService {

  private baseRoute = 'cardtype/';

  constructor(
    private http: HttpClient,
    private storage: StorageService
    ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  private store(o: Observable<CardTypeDto[]>): void{
    o.subscribe(data => {
      console.log('writing cardtypes to storage');
      this.storage.cardTypes(data);
    });
  }

  public getAll(): Observable<CardTypeDto[]>{
    const url = `${this._baseUrl}${this.baseRoute}all`;
    console.log(url);
    const cardTypes = this.storage.cardTypes();
    if (cardTypes == null){
      const obs = this.http.get<CardTypeDto[]>(url);
      this.store(obs);
      return obs;
    }
    else{
      console.log('found card types in storage');
      return of(cardTypes);
    }
  }
}

