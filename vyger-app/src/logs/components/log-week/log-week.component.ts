import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { utilities } from 'src/models/utilities';
import { LogExercise, LogEvaluation } from 'src/models/log-exercise';
import { LogsRepository } from 'src/logs/logs.repository';

@Component({
    selector: 'app-log-week',
    templateUrl: './log-week.component.html',
    styleUrls: ['./log-week.component.css']
})
export class LogWeekComponent implements OnInit
{
    ymd: string;
    exercises: LogExercise[];

    constructor(
        private activatedRoute: ActivatedRoute,
        private logRepository: LogsRepository)
    {
        this.ymd = utilities.getYMD(new Date());
    }

    ngOnInit()
    {
        this.activatedRoute.queryParams.subscribe((x: Params) =>
        {
            if (x.date)
            {
                this.exercises = null;

                this.ymd = x.date;

                this.loadExercises();
            }
            else
            {
                this.loadExercises();
            }
        });
    }

    private loadExercises = (): void =>
    {
        this.logRepository
            .getLogsFor(this.ymd)
            .then(this.onloadingExercises);
    }

    private onloadingExercises = (data: LogExercise[]): void =>
    {
        this.exercises = data.sort(LogExercise.compare);
    }

    //  crutch for sortablejs callback
    resequence = (): void =>
    {
        if (this.exercises)
        {
            for (let i = 0; i < this.exercises.length; i++)
            {
                this.exercises[i].sequence = i + 1;
            }

            this.logRepository.save();
        }
    }
}
