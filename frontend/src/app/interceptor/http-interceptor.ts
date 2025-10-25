import { HttpInterceptorFn } from '@angular/common/http';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {

  const clonedReq = req.clone({
    setHeaders: {
      'Content-Type': 'application/json' 
    }
  })
  return next(clonedReq);
};
