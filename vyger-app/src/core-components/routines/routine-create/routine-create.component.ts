import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Routine } from 'src/models/routine';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { PageTitleService } from 'src/services/page-title.service';
import { RoutineService } from 'src/services/routine.service';

@Component({
    selector: 'app-routine-create',
    templateUrl: './routine-create.component.html'
})
export class RoutineCreateComponent implements OnInit
{
    routine: Routine;
    saving: boolean;

    constructor(
        private router: Router,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private routineService: RoutineService) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Create Routine');

        let options = {
            name: '',
            sets: ['5RM-90%x5', '5RMx4']
        };

        this.routine = new Routine(options);

        this.updateBreadCrumbs();
    }

    cancel(): void
    {
        this.router.navigateByUrl('/routines');
    }

    save(): void
    {
        this.saving = true;

        this.routineService
            .addRoutine(this.routine)
            .then(() =>
            {
                this.saving = false;

                this.router.navigateByUrl('/routines/exercises/' + this.routine.id);
            });
    }

    private updateBreadCrumbs = () =>
    {
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Routines', '/Routines');
        this.breadCrumbService.add('Create Routine');
    };
}
