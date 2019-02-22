import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExerciseLog } from 'src/models/exercise-log';
import { utilities } from 'src/models/utilities';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { ExerciseLogService } from 'src/services/exercise-log.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';

@Component({
    selector: 'app-exercise-log-delete',
    templateUrl: './exercise-log-delete.component.html'
})
export class ExerciseLogDeleteComponent implements OnInit
{
    id: string;
    date: string;
    saving: boolean;
    exercise: ExerciseLog;
    exercises: ExerciseLog[];

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private exerciseLogService: ExerciseLogService)
    {
        this.date = utilities.getYMD(new Date());
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Exercise Logs');

        this.date = this.activatedRoute.snapshot.paramMap.get('date');

        this.id = this.activatedRoute.snapshot.queryParamMap.get('exercise');

        this.updateBreadCrumbs();

        this.exerciseLogService
            .getLogs()
            .then(this.onloadingExerciseLogs);
    }

    private onloadingExerciseLogs = (exercises: ExerciseLog[]): void =>
    {
        this.exercises = exercises;

        this.exercise = this.exercises.find(x => x.ymd == this.date && x.id == this.id);

        if (this.exercise == null)
        {
            let msg = 'Selected exercise could not be found'

            this.flashMessageService.danger(msg, true);

            this.navigateToExercises();
        }
    }

    cancel(): void
    {
        this.navigateToExercises();
    }

    delete(): void
    {
        this.saving = true;

        for (let i = 0; i < this.exercises.length; i++)
        {
            let exercise = this.exercises[i];

            if (exercise.id == this.exercise.id && exercise.ymd == this.date)
            {
                this.exercises.splice(i, 1);

                break;
            }
        }

        this.exerciseLogService
            .save()
            .then(() =>
            {
                this.saving = false;

                this.navigateToExercises();
            });
    }

    private navigateToExercises = (): void =>
    {
        this.router.navigateByUrl('/logs/exercises/' + this.date);
    };

    private updateBreadCrumbs = () =>
    {
        this.breadCrumbService.clear();
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Logs', '/logs/');
        this.breadCrumbService.add(this.date, '/logs/exercises/' + this.date);
        this.breadCrumbService.add('Delete Exercise');
    };
}
