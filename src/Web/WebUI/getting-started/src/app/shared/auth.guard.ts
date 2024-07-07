import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './services/auth.service';

export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    if (this.authService.isLoggedIn && !this.authService.isTokenExpired) {
      if (route.data['roles']) {
        if (this.userInRoles(route)) {
          return true;
        }
        else {
          // User doesn't have required permission, redirect to unauthorized page
          this.router.navigate(['/unauthorized']);
          return false;
        }
      }
      return true;
    }

    // Not logged in or token expired, redirect to login page with return url
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }

  private userInRoles(route: ActivatedRouteSnapshot): boolean {
    const expectedRoles = route.data['roles'];
    if (expectedRoles == undefined || this.authService.isInRoles(expectedRoles)) {
      return true;
    }
    else {
      return false;
    }
  }

}
