import { Injectable, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';

// @Injectable({
//   providedIn: 'root',
// })
// export class AuthGuard implements CanActivate {
//   constructor(private router: Router) {}

//   canActivate(
//     route: ActivatedRouteSnapshot,
//     state: RouterStateSnapshot
//   ): boolean {
//     const token = localStorage.getItem('token');

//     if (token) {
//       return true;
//     } else {
//       this.router.navigate(['/login']);
//       return false;
//     }
//   }
// }

// @Injectable({
//   providedIn: 'root',
// })
// export class RoleGuard implements CanActivate {
//   constructor(private router: Router) {}

//   canActivate(
//     route: ActivatedRouteSnapshot,
//     state: RouterStateSnapshot
//   ): boolean {
//     const user = JSON.parse(String(localStorage.getItem('user'))); // Parse user from localStorage
//     const allowedRoles = route.data['roles'] as Array<string>; // Get allowed roles from route data
//     if (user && user.role && allowedRoles.includes(user.role)) {
//       return true;
//     } else {
//       this.router.navigate(['/login']);
//       return false;
//     }
//   }
// }

export const AuthGuard: CanActivateFn = (
  next: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  // const authService: AuthService = inject(AuthService);
  const router: Router = inject(Router);

  const token = localStorage.getItem('token');

  if (token) {
    return true;
  } else {
    return router.navigate(['/login']);
  }
};

export const RoleGuard: CanActivateFn = (
  next: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  // const authService: AuthService = inject(AuthService);
  const router: Router = inject(Router);

  const user = JSON.parse(String(localStorage.getItem('user'))); // Parse user from localStorage

  const allowedRoles = next.data['roles'] as Array<string>; // Get allowed roles from route data
  if (user && user.role && allowedRoles.includes(user.role)) {
    return true;
  } else {
    return router.navigate(['/login']);
  }
};
