import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Routine } from 'src/models/routine';
import { utilities } from 'src/models/utilities';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';
import { RoutineService } from 'src/services/routine.service';


@Component({
    selector: 'app-routine-update',
    templateUrl: './routine-update.component.html'
})
export class RoutineUpdateComponent implements OnInit
{
    original: Routine;
    clone: Routine;
    saving: boolean;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private routineService: RoutineService) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Update Routine');

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.routineService
            .getRoutine(id)
            .then(this.onloadingRoutine);

        this.updateBreadCrumbs();
    }

    private onloadingRoutine = (routine: Routine): void =>
    {
        if (routine == null)
        {
            let msg = 'Selected routine could not be found'

            this.flashMessageService.danger(msg, true);

            this.router.navigateByUrl('/routines');
        }
        else
        {
            this.original = routine;

            this.clone = new Routine({ ...this.original });
        }
    }

    cancel(): void
    {
        this.router.navigateByUrl('/routines');
    }

    save(): void
    {
        const keys = ['properties'];

        utilities.extend(this.original, this.clone, keys);

        this.saving = true;

        this.routineService
            .save()
            .then(() =>
            {
                this.saving = false;
                this.router.navigateByUrl('/routines');
            });
    }

    private updateBreadCrumbs = () =>
    {
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Routines', '/Routines');
        this.breadCrumbService.add('Update Routine');
    };
}
