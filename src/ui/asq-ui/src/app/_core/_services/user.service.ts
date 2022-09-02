import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '@environments/environment';
import { UserDto } from '@models/domain/user-dto';
import { ImgDto } from '@app/_models/domain/img-dto';
import { UserTestDataService } from '../../_data/user-test-data-service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseRoute = 'user/';
  private useTestData = false;

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  constructor(
    private http: HttpClient,
    private userDataService: UserTestDataService
  ) { }

  public login(username: string, password: string): Observable<any>{
    const url = `${this._baseUrl}${this.baseRoute}authenticate`;
    console.log(url);
    return this.http.post<UserDto>(url, {
      Username: username,
      Password: password
    });
  }

  public create(user: UserDto): Observable<HttpResponse<any>>{
    const url = `${this._baseUrl}${this.baseRoute}create`;
    console.log(url);
    return this.http.post<HttpResponse<any>>(url, user);
  }

  public update(user: UserDto): Observable<HttpResponse<any>>{
    const url = `${this._baseUrl}${this.baseRoute}update`;
    console.log(url);
    return this.http.put<HttpResponse<any>>(url, user);
  }

  public testImg(img: ImgDto): Observable<any>{
    const url = `${this._baseUrl}${this.baseRoute}img/test`;
    console.log(url);
    return this.http.post<ImgDto>(url, img);
  }

  public get(guid: string): Observable<UserDto>{
    const url = `${this._baseUrl}${this.baseRoute}${guid}`;
    console.log(url);
    //if (this.useTestData){
      //return this.userDataService.user();
    //}
    return this.http.get<UserDto>(url);
  }

  public activate(guid: string): Observable<HttpResponse<any>>{
    const url = `${this._baseUrl}${this.baseRoute}activate/${guid}`;
    console.log(url);
    return this.http.get<HttpResponse<any>>(url);
  }
}
