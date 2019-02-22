import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Exercise } from 'src/models/exercise';
import { ExerciseLog } from 'src/models/exercise-log';
import { utilities } from 'src/models/utilities';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { ExerciseLogService } from 'src/services/exercise-log.service';
import { ExerciseService } from 'src/services/exercise.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';

@Component({
    selector: 'app-exercise-log-create',
    templateUrl: './exercise-log-create.component.html'
})
export class ExerciseLogCreateComponent implements OnInit
{
    date: string;
    saving: boolean;
    exercise: ExerciseLog;
    logs: ExerciseLog[];
    exercises: Exercise[];

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private exerciseService: ExerciseService,
        private exerciseLogService: ExerciseLogService)
    {
        this.date = utilities.getYMD(new Date());

        this.exercise = new ExerciseLog();

        this.exercise.id = null;
        this.exercise.group = null;
        this.exercise.category = null;
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Exercise Logs');

        this.date = this.activatedRoute.snapshot.paramMap.get('date');

        this.updateBreadCrumbs();

        this.exerciseService
            .getExercises()
            .then(this.onloadingExercises);

        this.exerciseLogService
            .getLogs()
            .then(this.onloadingExerciseLogs);
    }

    private onloadingExerciseLogs = (logs: ExerciseLog[]): void =>
    {
        this.logs = logs.filter(x => x.ymd == this.date);
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

        let log = new ExerciseLog(exercise);

        log.ymd = this.date;
        log.pattern = this.exercise.pattern;
        log.sequence = -1;

        this.exerciseLogService
            .add(log)
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
        this.breadCrumbService.add('Add Exercise');
    };
}
