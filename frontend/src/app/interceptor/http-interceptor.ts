import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);

  const clonedReq = req.clone({
    withCredentials: true,
    setHeaders: {
      'Content-Type': 'application/json'
    }
  })
  return next(clonedReq)
  // .pipe(
  //   catchError((error) => {
  //     if(error.status === 401){
  //       alert("Token Expired Redirecting to Login Page")
  //       localStorage.removeItem("token");
  //       router.navigate(['login']);
  //     }
  //     return throwError(() => error)
  //   })
  // );
};
