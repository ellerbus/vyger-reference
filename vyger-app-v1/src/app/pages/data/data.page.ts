import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { DataService } from '../../services/data.service';


@Component({
    selector: 'app-data',
    templateUrl: './data.page.html',
    styleUrls: ['./data.page.css']
})
export class DataPageComponent implements OnInit, OnDestroy {
    private returnUrl: string;
    private subscriber: Subscription;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        public dataService: DataService
    ) { }

    ngOnInit() {
        this.subscriber = this.route.queryParamMap.subscribe(x => {
            if (x.has('returnUrl')) {
                this.returnUrl = x.get('returnUrl');
            }
        });
        this.dataService.loadFiles()
            .then(this.redirect);
    }

    ngOnDestroy() {
        this.subscriber.unsubscribe();
    }

    private redirect = (): void => {
        var url = this.returnUrl || '/';

        this.router.navigateByUrl(url);
    }
}
