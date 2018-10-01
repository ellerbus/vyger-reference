import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { PartialObserver } from 'rxjs';

@Component({
    selector: 'app-side-menu',
    templateUrl: './side-menu.component.html',
    styleUrls: ['./side-menu.component.css']
})
export class SideMenuComponent implements OnInit, OnDestroy {
    private subscriber;

    open: boolean;

    constructor(
        private router: Router) { }

    ngOnInit() {
        this.subscriber = this.router
            .events
            .subscribe(x => {
                if (x instanceof NavigationEnd) {
                    this.open = false;
                }
            });
    }

    ngOnDestroy() {
        this.subscriber = null;
    }

}
