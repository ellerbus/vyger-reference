import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
    selector: 'app-navigation-exercises',
    templateUrl: './navigation-exercises.component.html'
})
export class NavigationExercisesComponent {

    constructor(
        private authenticationService: AuthenticationService) { }

    get isSignedIn(): boolean {
        return this.authenticationService.isSignedIn();
    }
}
