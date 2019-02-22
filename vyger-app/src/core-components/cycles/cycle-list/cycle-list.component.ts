import { Component, OnInit } from '@angular/core';
import { PageTitleService } from 'src/services/page-title.service';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { Cycle } from 'src/models/cycle';
import { CycleService } from 'src/services/cycle.service';

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
        this.cycles = data.sort(Cycle.compare);
    };

    private updateBreadCrumbs = () =>
    {
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Cycles');
    };
}