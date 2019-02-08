import { Component, OnInit } from '@angular/core';
import { PageTitleService } from 'src/services/page-title.service';
import { Cycle } from 'src/models/cycle';
import { CycleService } from 'src/services/cycle.service';

@Component({
    selector: 'app-cycle-list',
    templateUrl: './cycle-list.component.html',
    styleUrls: ['./cycle-list.component.css']
})
export class CycleListComponent implements OnInit
{
    cycles: Cycle[];

    constructor(
        private pageTitleService: PageTitleService,
        private CycleService: CycleService) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Cycles');

        this.CycleService
            .getCycles()
            .then(this.onloadingCycles);
    }

    private onloadingCycles = (data: Cycle[]) =>
    {
        this.cycles = data.sort(Cycle.compare);
    };
}
