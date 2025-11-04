import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth-service';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const authService = inject(AuthService);

  const clonedReq = req.clone({
    withCredentials: true,
    setHeaders: {
      'Content-Type': 'application/json'
    }
  })
  return next(clonedReq)
  .pipe(
    catchError((error) => {
      const isAuthApi = req.url.includes('/login') || req.url.includes("/register");
      if(error.status === 401 && !isAuthApi){
        alert("Token Expired Redirecting to Login Page")
        authService.fnLogout();
      }
      return throwError(() => error)
    })
  );
};
