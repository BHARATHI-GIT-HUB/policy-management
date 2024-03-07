import { Injectable, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const token = localStorage.getItem('token');

    if (token) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}

@Injectable({
  providedIn: 'root',
})
export class RoleGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const user = JSON.parse(String(localStorage.getItem('user'))); // Parse user from localStorage
    const allowedRoles = route.data['roles'] as Array<string>; // Get allowed roles from route data
    console.log(user, user.role, allowedRoles);
    if (user && user.role && allowedRoles.includes(user.role)) {
      console.log(allowedRoles, 'allowedroles');
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
