import { Component, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { FlashMessageService } from 'src/services/flash-message.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit
{
    constructor(
        private router: Router,
        private flashMessageService: FlashMessageService,
        private breadCrumbsService: BreadCrumbsService) { }

    ngOnInit()
    {
        this.router.events
            .pipe(filter(event => event instanceof NavigationStart))
            .subscribe(event =>
            {
                this.breadCrumbsService.clear();
            });

        this.router.events
            .pipe(filter(event => event instanceof NavigationEnd))
            .subscribe(event =>
            {
                this.flashMessageService.clear();

                window.scrollTo(0, 0);
            });
    }
}
