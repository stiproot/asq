import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { VidDto } from '../_models/domain/vid-dto';

@Injectable({
  providedIn: 'root'
})
export class VidService {

  private baseRoute = 'file/';

  constructor(
    private http: HttpClient
  ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  public uploadVid(formData: FormData): Observable<VidDto>{
    const url = `${this._baseUrl}${this.baseRoute}upload/vid`;
    console.log(url);
    return this.http.post<VidDto>(url, formData)
  }
}
