import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'app-navigation',
    templateUrl: './navigation.component.html',
    styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {

    constructor(
        private authenticationService: AuthenticationService) { }

    get isSignedIn(): boolean {
        return this.authenticationService.isSignedIn();
    }
}
