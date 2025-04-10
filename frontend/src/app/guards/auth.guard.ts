import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const token = localStorage.getItem('accessToken');
  if (token) {
    const decoded = jwtDecode(token);
    if (decoded) {
      const exp = decoded?.exp;
      const now = Date.now() / 1000;

      if (exp && exp > now) {
        if (!authService.user()) authService.loadUser().subscribe({
          next: (user) => {
            if (!user) router.navigate(['login']);
          },
          error: () => {
            router.navigate(['login']);
          },
        });
        return true;
      }
    }
  }

  router.navigate(['login']);
  return false;
};
