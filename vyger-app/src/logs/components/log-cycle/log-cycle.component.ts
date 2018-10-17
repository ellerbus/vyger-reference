import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { LogsRepository } from 'src/logs/logs.repository';
import { CyclesRepository } from 'src/cycles/cycles.repository';
import { LogExercise } from 'src/models/log-exercise';
import { utilities } from 'src/models/utilities';
import { Cycle } from 'src/models/cycle';
import { PageTitleService } from 'src/page-title/page-title.service';
import { LogDate } from 'src/models/log-date';
import { ExercisesRepository } from 'src/exercises/exercises.repository';
import { Exercise } from 'src/models/exercise';


@Component({
    selector: 'app-log-cycle',
    templateUrl: './log-cycle.component.html',
    styleUrls: ['./log-cycle.component.css']
})
export class LogCycleComponent implements OnInit
{
    saving: boolean;
    logdate: LogDate;
    ymd: string;
    today: string;
    prev: string;
    next: string;
    week: number;
    day: number;
    cycle: Cycle;
    exercises: LogExercise[];

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private cyclesRepository: CyclesRepository,
        private logsRepository: LogsRepository,
        private exercisesRepository: ExercisesRepository)
    {
        this.logdate = new LogDate(new Date());

        this.week = 1;

        this.day = 1;

        this.updateDates();
    }

    ngOnInit()
    {
        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.week = +this.activatedRoute.snapshot.queryParamMap.get('week');

        this.day = +this.activatedRoute.snapshot.queryParamMap.get('day');

        this.cyclesRepository
            .getCycle(id)
            .then(this.onloadingCycle);

        this.activatedRoute.queryParams.subscribe((x: Params) =>
        {
            if (x.date)
            {
                let dt = utilities.toDate(x.date);

                this.logdate = new LogDate(dt);

                this.updateDates();
            }
            else
            {
                this.updateDates();
            }
        });
    }

    private onloadingCycle = (cycle: Cycle): void =>
    {
        if (cycle == null)
        {
            this.router.navigateByUrl('/cycles');
        }
        else
        {
            this.cycle = cycle;

            this.exercises = this.cycle
                .exercises
                .filter(x => x.week == this.week && x.day == this.day)
                .map(x => new LogExercise({ ...x, sets: [...x.plan], ymd: this.ymd }))
                .sort(LogExercise.compare);

            this.updateTitle();
        }
    }

    private updateDates = (): void =>
    {
        this.ymd = utilities.getYMD(this.logdate.date);

        this.today = utilities.getYMD(new Date());

        this.prev = utilities.getYMD(utilities.addDays(this.logdate.date, -1));

        this.next = utilities.getYMD(utilities.addDays(this.logdate.date, 1));

        if (this.today == this.ymd)
        {
            this.today = null;
        }

        this.updateTitle();
    }

    private updateTitle = (): void =>
    {
        if (this.cycle)
        {
            const subtitle = 'log cycle date=' + this.ymd + ' week=' + this.week + ' day=' + this.day;

            this.pageTitleService.setTitle(this.cycle.name, subtitle);
        }
    }

    updateSets(log: LogExercise, tmp: string)
    {
        log.sets = tmp.split(/, */);
    }

    remove(idx: number)
    {
        this.exercises.splice(idx, 1);
    }

    cancel(): void
    {
        const queryParams = { week: this.week, day: this.day };

        let url = this.router.createUrlTree(['/cycles/exercises/' + this.cycle.id], { queryParams });

        this.router.navigateByUrl(url);
    }

    save()
    {
        this.saving = true;

        this.logsRepository.getLogs().then(this.savingLogs);
    }

    private savingLogs = (all: LogExercise[]) =>
    {
        let lookup = this.getExerciseLookup();

        for (let i = 0; i < this.exercises.length; i++)
        {
            let log = this.exercises[i];

            let found = all.some(x => x.ymd == log.ymd && x.id == log.id);

            if (!found && log.sets && log.sets.length > 0)
            {
                let ex = lookup[log.id];

                let copy = new LogExercise({ ...ex, ...log, ymd: this.ymd });

                copy.updateOneRepMax();

                all.push(copy);
            }
        }

        this.logsRepository.save().then(this.onsavingCycle);
    }

    private onsavingCycle = () =>
    {
        this.cycle.lastLogged = this.week + ":" + this.day;

        this.cyclesRepository
            .save()
            .then(() =>
            {
                const queryParams = { date: this.ymd };

                let url = this.router.createUrlTree(['/logs'], { queryParams });

                this.router.navigateByUrl(url);
            });
    }

    private getExerciseLookup = (): { [key: string]: Exercise } =>
    {
        let lookup: { [key: string]: Exercise } = {};

        for (let i = 0; i < this.exercises.length; i++)
        {
            let e = this.exercises[i];

            lookup[e.id] = e;
        }

        return lookup;
    }
}
