import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Routine } from 'src/models/routine';
import { RoutineExercise } from 'src/models/routine-exercise';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';
import { RoutineService } from 'src/services/routine.service';

@Component({
    selector: 'app-routine-exercise-delete',
    templateUrl: './routine-exercise-delete.component.html'
})
export class RoutineExerciseDeleteComponent implements OnInit
{
    saving: boolean;
    routine: Routine;
    exercise: RoutineExercise;
    day: number;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private routineService: RoutineService)
    {
        this.day = 1;
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('RoutineExercises');

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.day = +this.activatedRoute.snapshot.queryParamMap.get('day');

        this.loadTitle();

        this.updateBreadCrumbs();

        this.routineService
            .getRoutine(id)
            .then(this.onloadingRoutine);
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
            this.routine = routine;

            const exercise = this.activatedRoute.snapshot.queryParamMap.get('exercise');

            this.exercise = this.routine.exercises.find(x => x.id == exercise);

            this.loadTitle();

            this.updateBreadCrumbs();
        }
    }

    private loadTitle = (): void =>
    {
        if (this.routine)
        {
            //  const subtitle = 'clones week=' + this.week + ' day=' + this.day;

            //  this.pageTitleService.setTitle(this.routine.name, subtitle);
        }
    }

    cancel(): void
    {
        this.navigateToExercises();
    }

    delete(): void
    {
        this.saving = true;

        for (let i = 0; i < this.routine.exercises.length; i++)
        {
            let exercise = this.routine.exercises[i];

            if (exercise.id == this.exercise.id && exercise.day == this.day)
            {
                this.routine.exercises.splice(i, 1);

                i -= 1;
            }
        }

        this.routineService
            .save()
            .then(() =>
            {
                this.saving = false;

                this.navigateToExercises();
            });
    }

    private navigateToExercises = (): void =>
    {
        const queryParams = { week: 1, day: this.day };

        let url = this.router.createUrlTree(['/routines/exercises/', this.routine.id], { queryParams });

        this.router.navigateByUrl(url);

    };

    private updateBreadCrumbs = () =>
    {
        if (this.routine)
        {
            let filter = 'Day=' + this.day;

            this.breadCrumbService.clear();
            this.breadCrumbService.add('Home', '/');
            this.breadCrumbService.add('Routines', '/routines');
            this.breadCrumbService.add(this.routine.name, '/routines/exercises/' + this.routine.id);
            this.breadCrumbService.add('Delete Exercise', null, filter);
        }
    };
}
