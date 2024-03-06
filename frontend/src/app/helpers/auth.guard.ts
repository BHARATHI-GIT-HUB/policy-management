import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem('token');
  const router = inject(Router);
  if (token) return true;

  return router.navigate(['/login']);
};
export const adminGuard: CanActivateFn = (route, state) => {
  const user: any = JSON.parse(String(localStorage.getItem('user')));

  if (user.Role === 'Admin') {
    return true;
  }
  const router = inject(Router);

  return router.navigate(['/login']);
};
export const providerGuard: CanActivateFn = (route, state) => {
  const user: any = JSON.parse(String(localStorage.getItem('user')));
  if (user.Role === 'Provider') {
    return true;
  }
  const router = inject(Router);

  return router.navigate(['/login']);
};
export const clientGuard: CanActivateFn = (route, state) => {
  const user: any = JSON.parse(String(localStorage.getItem('user')));
  if (user.Role === 'Client') {
    return true;
  }
  const router = inject(Router);

  return router.navigate(['/login']);
};

export const agentGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem('token');

  const user: any = localStorage.getItem('user');
  if (token && user.Role === 'Agent') {
    return true;
  }
  const router = inject(Router);

  return router.navigate(['/login']);
};
