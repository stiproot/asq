import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { ImgDto } from '../_models/domain/img-dto';

@Injectable({
  providedIn: 'root'
})
export class ImgService {

  private baseRoute = 'file/';

  constructor(
    private http: HttpClient
  ) { }

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  public uploadImg(formData: FormData): Observable<ImgDto>{
    const url = `${this._baseUrl}${this.baseRoute}upload/img`;
    console.log(url);
    return this.http.post<ImgDto>(url, formData)
  }
}
