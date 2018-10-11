import { Component, OnInit } from '@angular/core';
import { PageTitleService } from 'src/page-title/page-title.service';
import { AuthenticationService } from 'src/services/authentication.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    constructor(
        private pageTitleService: PageTitleService,
        private authenticationService: AuthenticationService) { }

    ngOnInit() {
        const title = 'Hello ' + this.authenticationService.user.givenName;

        this.pageTitleService.setTitle(title);
    }
}
