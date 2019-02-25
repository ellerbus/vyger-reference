import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Exercise } from 'src/models/exercise';
import { Routine } from 'src/models/routine';
import { RoutineExercise } from 'src/models/routine-exercise';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { ExerciseService } from 'src/services/exercise.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';
import { RoutineService } from 'src/services/routine.service';

@Component({
    selector: 'app-routine-exercise-create',
    templateUrl: './routine-exercise-create.component.html'
})
export class RoutineExerciseCreateComponent implements OnInit
{
    saving: boolean;
    routine: Routine;
    exercise: RoutineExercise;
    exercises: Exercise[];
    day: number;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private exerciseService: ExerciseService,
        private routineService: RoutineService)
    {
        this.day = 1;
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Routine Exercises');

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.day = +this.activatedRoute.snapshot.queryParamMap.get('day');

        this.updateBreadCrumbs();

        this.routineService
            .getRoutine(id)
            .then(this.onloadingRoutine);

        this.exerciseService
            .getExercises()
            .then(this.onloadingExercises);
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

            this.exercise = new RoutineExercise();

            this.exercise.id = null;
            this.exercise.group = null;
            this.exercise.category = null;
            this.exercise.day = this.day;
            this.exercise.pattern = this.routine.pattern;

            this.updateBreadCrumbs();
        }
    }

    private onloadingExercises = (exercises: Exercise[]): void =>
    {
        this.exercises = exercises;
    }

    cancel(): void
    {
        this.navigateToExercises();
    }

    save(): void
    {
        this.saving = true;

        let exercise = this.exercises.find(x => x.id == this.exercise.id);

        for (let week = 1; week <= this.routine.weeks; week++)
        {
            let rex = new RoutineExercise(exercise);

            rex.week = week;
            rex.day = this.day;
            rex.sequence = -1;
            rex.pattern = this.exercise.pattern;

            this.routine.exercises.push(rex);
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
            this.breadCrumbService.add('Add Exercise', null, filter);
        }
    };
}
