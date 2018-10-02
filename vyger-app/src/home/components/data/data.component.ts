import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { PageTitleService } from 'src/page-title/page-title.service';
import { DataService } from 'src/services/data.service';

@Component({
    selector: 'app-data',
    templateUrl: './data.component.html',
    styleUrls: ['./data.component.css']
})
export class DataComponent implements OnInit {

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private dataService: DataService) { }

    ngOnInit() {
        this.pageTitleService.setTitle('Loading Data ...');

        let goto = this.activatedRoute.snapshot.queryParamMap.get('returnUrl') || '/home';

        this.dataService
            .loadFiles()
            .then(() => this.router.navigateByUrl(goto))
    }

}
