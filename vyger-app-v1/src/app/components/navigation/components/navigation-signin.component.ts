import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
    selector: 'app-navigation-signin',
    templateUrl: './navigation-signin.component.html'
})
export class NavigationSignInComponent implements OnInit, OnDestroy {
    private returnUrl: string;
    private subscriber: Subscription;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private authenticationService: AuthenticationService) { }

    ngOnInit() {
        this.subscriber = this.route.queryParamMap.subscribe(x => {
            if (x.has('returnUrl')) {
                this.returnUrl = x.get('returnUrl');
            }
        });
    }

    ngOnDestroy() {
        this.subscriber.unsubscribe();
    }

    get isSignedIn(): boolean {
        return this.authenticationService.isSignedIn();
    }

    signIn(): void {
        this.authenticationService
            .signIn()
            .then(this.redirectHome);
    }

    private redirectHome = (): void => {
        if (this.router.url === '/signin') {
            var url = this.returnUrl || '/';

            this.router.navigateByUrl(url);
        }
    }
}
