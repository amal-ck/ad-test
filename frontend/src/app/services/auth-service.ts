import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ApiData } from './api-data';
import { catchError, map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  ísLoggedIn: boolean = false;

  constructor(private router: Router,
    private api: ApiData
  ) { }

  fnLogin() {
    this.ísLoggedIn = true;
    this.router.navigate([""])
  }

  fnLogout() {
    this.ísLoggedIn = false;
    this.router.navigate(["login"])
  }
}
