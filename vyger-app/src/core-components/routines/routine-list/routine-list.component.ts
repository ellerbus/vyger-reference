import { Component, OnInit } from '@angular/core';
import { Routine } from 'src/models/routine';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { PageTitleService } from 'src/services/page-title.service';
import { RoutineService } from 'src/services/routine.service';

@Component({
    selector: 'app-routine-list',
    templateUrl: './routine-list.component.html'
})
export class RoutineListComponent implements OnInit
{
    routines: Routine[];

    constructor(
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private routineService: RoutineService) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Routines');

        this.routineService
            .getRoutines()
            .then(this.onloadingRoutines);

        this.updateBreadCrumbs();
    }

    private onloadingRoutines = (data: Routine[]) =>
    {
        this.routines = data.sort(Routine.compare);
    };

    private updateBreadCrumbs = () =>
    {
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Routines');
    };
}
