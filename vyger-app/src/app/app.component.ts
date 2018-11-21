import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { FlashMessageService } from 'src/services/flash-message.service';
import { filter } from 'rxjs/operators';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit
{
    constructor(
        private router: Router,
        private flashMessageService: FlashMessageService) { }

    ngOnInit()
    {
        this.router.events
            .pipe(filter(event => event instanceof NavigationEnd))
            .subscribe(event =>
            {
                this.flashMessageService.messages = [];

                window.scrollTo(0, 0);
            });
    }
}
