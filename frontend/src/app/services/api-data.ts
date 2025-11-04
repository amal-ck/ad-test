import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
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
  
  getGames(pageNumber: number, pageSize: number):Observable<any>{
    return this.http.get(this.baseUrl + `game/getgames?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  checkLogin():Observable<boolean>{
    return this.http.get(this.baseUrl + "login/checkLogin").pipe(
      map((res:any) => res.isLoggedIn),
      catchError(() => of(false))
    );
  }
}
