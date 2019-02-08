import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { PageTitleService } from 'src/services/page-title.service';
import { utilities } from 'src/models/utilities';
import { LogDate } from 'src/models/log-date';

@Component({
    selector: 'app-log-week-header',
    templateUrl: './log-week-header.component.html',
    styleUrls: ['./log-week-header.component.css']
})
export class LogWeekHeaderComponent implements OnInit
{
    logdate: LogDate;
    today: string;
    prev: string;
    next: string;

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

            this.updateHeader();
        });
    }

    private updateHeader = (): void =>
    {
        let ymd = utilities.getYMD(this.logdate.date);

        this.today = utilities.getYMD(new Date());

        this.prev = utilities.getYMD(utilities.addDays(this.logdate.date, -7));

        this.next = utilities.getYMD(utilities.addDays(this.logdate.date, 7));

        if (this.today == ymd)
        {
            this.today = null;
        }

        this.pageTitleService.setTitle(ymd);
    }
}
