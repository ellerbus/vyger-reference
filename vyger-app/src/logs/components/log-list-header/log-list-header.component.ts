import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { PageTitleService } from 'src/page-title/page-title.service';
import { utilities } from 'src/models/utilities';
import { LogDate } from 'src/models/log-date';

@Component({
    selector: 'app-log-list-header',
    templateUrl: './log-list-header.component.html',
    styleUrls: ['./log-list-header.component.css']
})
export class LogListHeaderComponent implements OnInit
{
    logdate: LogDate;

    constructor(
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService)
    {
        this.logdate = new LogDate(new Date());
    }

    ngOnInit()
    {
        this.activatedRoute.queryParams.subscribe((x: Params) =>
        {
            if (x.date)
            {
                let dt = utilities.toDate(x.date);

                this.logdate = new LogDate(dt);
            }

            this.updateTitle();
        });
    }

    private updateTitle = (): void =>
    {
        let ymd = utilities.getYMD(this.logdate.date);

        this.pageTitleService.setTitle(ymd);
    }
}
