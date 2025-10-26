import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);

  const clonedReq = req.clone({
    setHeaders: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem("token") || ''}`
    }
  })
  return next(clonedReq).pipe(
    catchError((error) => {
      if(error.status === 401){
        localStorage.removeItem("token");
        router.navigate(['login']);
      }
      return throwError(() => error)
    })
  );
};
