import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
    selector: 'app-navigation-loggedin',
    templateUrl: './navigation-loggedin.component.html'
})
export class NavigationLoggedInComponent {

    constructor(
        private authenticationService: AuthenticationService) { }

    get isSignedIn(): boolean {
        return this.authenticationService.isSignedIn();
    }

    get imageUrl(): string {
        if (this.isSignedIn && this.authenticationService.user) {
            return this.authenticationService.user.imageUrl;
        }
        return null;
    }
}
