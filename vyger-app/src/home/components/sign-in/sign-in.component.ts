import { Component, OnInit } from '@angular/core';
import { PageTitleService } from 'src/page-title/page-title.service';
import { AuthenticationService } from 'src/services/authentication.service';

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

    constructor(
        private pageTitleService: PageTitleService,
        private authenticationService: AuthenticationService) { }

    ngOnInit() {
        this.pageTitleService.setTitle('Sign In');
    }
}
