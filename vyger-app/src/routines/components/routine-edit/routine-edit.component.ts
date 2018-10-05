import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Routine } from '../../../models/routine';
import { RoutinesRepository } from '../../routines.repository';
import { PageTitleService } from '../../../page-title/page-title.service';
import { utilities } from '../../../models/utilities';

@Component({
    selector: 'app-routine-edit',
    templateUrl: './routine-edit.component.html',
    styleUrls: ['./routine-edit.component.css']
})
export class RoutineEditComponent implements OnInit
{
    routine: Routine;
    clone: Routine;
    saving: boolean;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private routinesRepository: RoutinesRepository) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Edit Routine');

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.routinesRepository
            .getRoutine(id)
            .then(this.onloadingRoutine);
    }

    private onloadingRoutine = (routine: Routine): void =>
    {
        if (routine == null)
        {
            this.router.navigateByUrl('/routines');
        }
        else
        {
            this.routine = routine;

            this.clone = { ...this.routine };
        }
    }

    cancel(): void
    {
        this.router.navigateByUrl('/routines');
    }

    save(): void
    {
        const keys = ['name', 'week', 'days'];

        utilities.extend(this.routine, this.clone, keys);

        this.saving = true;

        this.routinesRepository
            .save()
            .then(() =>
            {
                this.saving = false;
                this.router.navigateByUrl('/routines/exercises/' + this.routine.id);
            });
    }
}
