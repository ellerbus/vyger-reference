import { Component, OnInit } from '@angular/core';
import { Routine } from 'src/models/routine';
import { Router, ActivatedRoute } from '@angular/router';

import { PageTitleService } from 'src/services/page-title.service';
import { RoutineExercise } from 'src/models/routine-exercise';
import { ExerciseService } from 'src/services/exercise.service';
import { RoutineService } from 'src/services/routine.service';

@Component({
    selector: 'app-routine-exercise-edit',
    templateUrl: './routine-exercise-edit.component.html',
    styleUrls: ['./routine-exercise-edit.component.css']
})
export class RoutineExerciseEditComponent implements OnInit
{
    routine: Routine;
    exercise: RoutineExercise;
    clone: RoutineExercise;
    saving: boolean;
    week: number;
    day: number;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private RoutineService: RoutineService,
        private ExerciseService: ExerciseService)
    {
    }

    ngOnInit()
    {
        const id = this.activatedRoute.snapshot.paramMap.get('id');

        if (this.activatedRoute.snapshot.queryParamMap.has('week'))
        {
            this.week = +this.activatedRoute.snapshot.queryParamMap.get('week');
        }

        if (this.activatedRoute.snapshot.queryParamMap.has('day'))
        {
            this.day = +this.activatedRoute.snapshot.queryParamMap.get('day');
        }

        this.RoutineService
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

            this.pageTitleService.setTitle(this.routine.name, 'editing exercise');

            const exerciseId = this.activatedRoute.snapshot.paramMap.get('exercise');

            const list = this.routine.exercises.filter(x => x.id == exerciseId && x.week == this.week && x.day == this.day);

            if (list && list.length == 1)
            {
                this.exercise = list[0];

                this.clone = <RoutineExercise>{ ...this.exercise };
            }
            else
            {
                this.router.navigateByUrl('/routines/exercises/' + this.routine.id);
            }
        }
    }

    cancel(): void
    {
        const queryParams = { week: this.exercise.week, day: this.exercise.day };

        let url = this.router.createUrlTree(['/routines/exercises/', this.routine.id], { queryParams });

        this.router.navigateByUrl(url);
    }

    remove()
    {
        let i = this.routine.exercises.length;

        while (i--)
        {
            let e = this.routine.exercises[i];

            if (this.exercise.id == e.id && this.day == e.day)
            {
                this.routine.exercises.splice(i, 1);
            }
        }

        this.RoutineService
            .save()
            .then(() =>
            {
                this.cancel();
            });
    }

    save()
    {
        this.saving = true;

        this.exercise.sets = this.clone.sets;

        this.RoutineService
            .save()
            .then(() =>
            {
                this.cancel();
            });
    }
}

