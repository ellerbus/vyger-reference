import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from 'src/services/authentication.service';

@Injectable()
export class AuthenticationGuard implements CanActivate {

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.authenticationService.isSignedIn()) {
            // logged in so return true
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigateByUrl('/home/sign-in', { queryParams: { returnUrl: state.url } });

        return false;
    }
}
