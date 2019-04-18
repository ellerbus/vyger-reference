import { Component, OnInit } from '@angular/core';
import { Cycle } from 'src/models/cycle';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { CycleService } from 'src/services/cycle.service';
import { PageTitleService } from 'src/services/page-title.service';

@Component({
    selector: 'app-cycle-list',
    templateUrl: './cycle-list.component.html'
})
export class CycleListComponent implements OnInit
{
    cycles: Cycle[];

    constructor(
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private cycleService: CycleService) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Cycles');

        this.cycleService
            .getCycles()
            .then(this.onloadingCycles);

        this.updateBreadCrumbs();
    }

    private onloadingCycles = (data: Cycle[]) =>
    {
        let end = Math.min(4, data.length);

        this.cycles = data.sort(Cycle.compare).slice(0, end);
    };

    private updateBreadCrumbs = () =>
    {
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Cycles');
    };

    getParams(cycle: Cycle): any
    {
        let [week, day] = this.getLastLogged(cycle);

        if (week == 0)
        {
            week = 1;
        }

        if (day == 0)
        {
            day = 1;
        }
        else if (day + 1 > cycle.days)
        {
            week += 1;
            day = 1;
        }
        else
        {
            day += 1;
        }

        if (week > cycle.weeks)
        {
            week = 1;
            day = 1;
        }

        return { week, day };
    }

    private getLastLogged = (cycle: Cycle): number[] =>
    {
        let logged = cycle.lastLogged || '0:0';

        let week = +(logged.split(':').shift());
        let day = +(logged.split(':').pop());

        return [week, day];
    }
}
