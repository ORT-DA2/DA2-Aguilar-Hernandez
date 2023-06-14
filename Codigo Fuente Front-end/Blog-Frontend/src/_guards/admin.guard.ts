import { Injectable, Inject } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../_services/authentication.service';
import { User } from '../_type/user';

@Injectable({
  providedIn: 'root',
})
export class AdminGuard implements CanActivate {
  user: User | null = null;
  constructor(
    @Inject(AuthenticationService) private authService: AuthenticationService,
    private router: Router
  ) {
    this.authService.user$.subscribe((user) => (this.user = user));
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (
      this.authService.isAuthenticated() &&
      this.user?.roles.some((role: any) => role.role === 1)
    ) {
      return true;
    } else {
      this.router.navigate(['/unauthorized']);
      return false;
    }
  }
}
