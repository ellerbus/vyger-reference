import { Component, OnInit } from '@angular/core';
import { Routine } from 'src/models/routine';
import { Router, ActivatedRoute } from '@angular/router';

import { PageTitleService } from 'src/page-title/page-title.service';
import { RoutineExercise } from 'src/models/routine-exercise';
import { Exercise } from 'src/models/exercise';
import { ExercisesRepository } from 'src/exercises/exercises.repository';
import { RoutinesRepository } from '../../routines.repository';

@Component({
    selector: 'app-routine-exercise-add',
    templateUrl: './routine-exercise-add.component.html',
    styleUrls: ['./routine-exercise-add.component.css']
})
export class RoutineExerciseAddComponent implements OnInit
{
    routine: Routine;
    exercise: RoutineExercise;
    saving: boolean;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private routinesRepository: RoutinesRepository,
        private exercisesRepository: ExercisesRepository)
    {
        this.exercise = new RoutineExercise();
        this.exercise.id = null;
        this.exercise.group = null;
        this.exercise.category = null;
        this.exercise.week = 1;
        this.exercise.day = 1;
        this.exercise.sets = ['5RM90%x5', '5RM90%x5'];
    }

    ngOnInit()
    {
        const id = this.activatedRoute.snapshot.paramMap.get('id');

        if (this.activatedRoute.snapshot.queryParamMap.has('day'))
        {
            this.exercise.day = +this.activatedRoute.snapshot.queryParamMap.get('day');
        }

        if (this.activatedRoute.snapshot.queryParamMap.has('week'))
        {
            this.exercise.week = +this.activatedRoute.snapshot.queryParamMap.get('week');
        }

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

            this.exercise.sets = this.routine.sets;

            this.pageTitleService.setTitle(this.routine.name);
            this.pageTitleService.setSubTitle('adding exercise');
        }
    }

    cancel(): void
    {
        const queryParams = { week: this.exercise.week, day: this.exercise.day };

        let url = this.router.createUrlTree(['/routines/exercises/', this.routine.id], { queryParams });

        this.router.navigateByUrl(url);
    }

    save()
    {
        this.saving = true;

        this.exercisesRepository
            .getExercise(this.exercise.id)
            .then(this.onsavingRoutineExercise)
            .then(() =>
            {
                this.cancel();
            });
    }

    private onsavingRoutineExercise = (exercise: Exercise): Promise<any> =>
    {
        const day = this.exercise.day;

        for (let week = 0; week < this.routine.weeks; week++)
        {
            const clone = {
                ...this.exercise,
                ...exercise,
                week: week + 1,
                day
            };

            let re = new RoutineExercise(clone);

            this.routine.exercises.push(re);
        }

        return this.routinesRepository.save();
    }
}

