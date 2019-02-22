import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cycle } from 'src/models/cycle';
import { Routine } from 'src/models/routine';
import { utilities } from 'src/models/utilities';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { CycleService } from 'src/services/cycle.service';
import { PageTitleService } from 'src/services/page-title.service';
import { RoutineService } from 'src/services/routine.service';

@Component({
    selector: 'app-cycle-create',
    templateUrl: './cycle-create.component.html'
})
export class CycleCreateComponent implements OnInit
{
    routines: Routine[];
    routine: Routine;
    saving: boolean;

    constructor(
        private router: Router,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private routineService: RoutineService,
        private cycleService: CycleService)
    {
        this.routine = null;
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Create Cycle');

        this.routineService
            .getRoutines()
            .then(this.onloadingRoutines);

        this.updateBreadCrumbs();
    }

    private onloadingRoutines = (data: Routine[]) =>
    {
        this.routines = data.sort(Routine.compare);
    }

    cancel(): void
    {
        this.router.navigateByUrl('/cycles');
    }

    save(): void
    {
        this.saving = true;

        const id = utilities.generateId('c', 3);

        let cycle = new Cycle({ ...this.routine, id });

        this.cycleService
            .add(cycle)
            .then(() =>
            {
                this.saving = false;
                this.router.navigateByUrl('/cycles/inputs/' + cycle.id);
            });
    }

    private updateBreadCrumbs = () =>
    {
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Cycles', '/Cycles');
        this.breadCrumbService.add('Create Cycle');
    };
}
