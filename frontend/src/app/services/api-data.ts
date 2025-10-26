import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiData {
  constructor(private http: HttpClient){}
  baseUrl = environment.baseUrl;

  getTest():Observable<any>{
    return this.http.get(this.baseUrl + "login/test", {responseType : 'text'});
  }

  registration(reqData: any):Observable<any>{
    return this.http.post(this.baseUrl + "login/register", reqData);
  }

  login(reqData: any):Observable<any>{
    return this.http.post(this.baseUrl + "login/login", reqData);
  }

  userData():Observable<any>{
    return this.http.get(this.baseUrl + "login/userData");
  }
}
