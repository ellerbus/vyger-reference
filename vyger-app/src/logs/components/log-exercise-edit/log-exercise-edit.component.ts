import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { PageTitleService } from 'src/page-title/page-title.service';
import { LogExercise } from 'src/models/log-exercise';
import { ExercisesRepository } from 'src/exercises/exercises.repository';
import { LogsRepository } from '../../logs.repository';

@Component({
    selector: 'app-log-exercise-edit',
    templateUrl: './log-exercise-edit.component.html',
    styleUrls: ['./log-exercise-edit.component.css']
})
export class LogExerciseEditComponent implements OnInit
{
    exercise: LogExercise;
    clone: LogExercise;
    saving: boolean;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private logsRepository: LogsRepository)
    {
    }

    ngOnInit()
    {
        const id = this.activatedRoute.snapshot.paramMap.get('id');

        const ymd = this.activatedRoute.snapshot.queryParamMap.get('date');

        this.logsRepository
            .getLogFor(ymd, id)
            .then(this.onloadingLog);
    }

    private onloadingLog = (log: LogExercise): void =>
    {
        if (log == null)
        {
            this.router.navigateByUrl('/logs');
        }
        else
        {
            this.exercise = log;

            this.pageTitleService.setTitle(this.exercise.ymd);
            this.pageTitleService.setSubTitle('adding exercise');

            this.clone = <LogExercise>{ ...this.exercise };
        }
    }

    remove(): void
    {
        this.logsRepository.getLogs()
            .then((logs) =>
            {
                let i = logs.length;

                while (i--)
                {
                    let log = logs[i];

                    if (log.ymd == this.exercise.ymd && log.id == this.exercise.id)
                    {
                        logs.splice(i, 1);
                        break;
                    }
                }
            })
            .then(() =>
            {
                this.logsRepository.save();
                this.cancel();
            });
    }

    cancel(): void
    {
        const queryParams = { date: this.exercise.ymd };

        let url = this.router.createUrlTree(['/logs/'], { queryParams });

        this.router.navigateByUrl(url);
    }

    save()
    {
        this.saving = true;

        this.exercise.sets = this.clone.sets;

        this.logsRepository
            .save()
            .then(() =>
            {
                this.cancel();
            });
    }
}

