import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'app-signin',
    templateUrl: './signin.page.html',
    styleUrls: ['./signin.page.css']
})
export class SignInPageComponent implements OnInit, OnDestroy {
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
        var url = this.returnUrl || '/';

        this.router.navigateByUrl(url);
    }
}
