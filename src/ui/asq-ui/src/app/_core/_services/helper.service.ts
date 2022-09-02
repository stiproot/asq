import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  constructor() { }

  public clone<T>(v: T): T{
    return JSON.parse(JSON.stringify(v)) as T;
  }
}
