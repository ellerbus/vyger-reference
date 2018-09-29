import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { DataService } from '../services/data.service';

@Injectable()
export class DataGuard implements CanActivate {

    constructor(
        private router: Router,
        private dataService: DataService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.dataService.hasUserData()) {
            //  we have user data
            return true;
        }

        //  we don't have user data yet
        this.router.navigateByUrl('/data', { queryParams: { returnUrl: state.url } });

        return false;
    }
}
