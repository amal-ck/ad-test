import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth-service';
import { ApiData } from '../services/api-data';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router)
  const authService = inject(AuthService)
  const api = inject(ApiData)
  
  if(api.checkLogin()){
    return true;
  }
  router.navigateByUrl('login')
  return false;
};
