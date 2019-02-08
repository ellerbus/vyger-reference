import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PageTitleService } from 'src/services/page-title.service';
import { ExercisesRepository } from 'src/exercises/exercises.repository';
import { LogsRepository } from '../../logs.repository';
import { LogExercise } from 'src/models/log-exercise';
import { Exercise } from 'src/models/exercise';
import { utilities } from 'src/models/utilities';
import { WorkoutSet } from 'src/models/workout-set';

@Component({
    selector: 'app-log-import',
    templateUrl: './log-import.component.html',
    styleUrls: ['./log-import.component.css']
})
export class LogImportComponent implements OnInit
{
    saving: boolean;
    logs: LogExercise[];
    exercises: Exercise[];

    constructor(
        private router: Router,
        private pageTitleService: PageTitleService,
        private logsRepository: LogsRepository,
        private exercisesRepository: ExercisesRepository) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Quick Import');

        this.logs = [];

        this.exercisesRepository
            .getExercises()
            .then(x => this.exercises = x.sort(Exercise.compare));
    }

    add()
    {
        let ymd = utilities.getYMD(new Date());

        if (this.logs.length > 0)
        {
            let idx = this.logs.length - 1;

            ymd = this.logs[idx].ymd;
        }

        let log = new LogExercise({ ymd });

        log.sequence = this.logs.length + 1;

        this.logs.push(log);
    }

    updateSets(log: LogExercise, tmp: string)
    {
        log.sets = tmp.split(/, */);
    }

    cancel(): void
    {
        this.router.navigateByUrl('/logs');
    }

    save()
    {
        this.saving = true;

        this.logsRepository.getLogs().then(this.savingLogs);
    }

    private savingLogs = (all: LogExercise[]) =>
    {
        let lookup = this.getExerciseLookup();

        for (let i = 0; i < this.logs.length; i++)
        {
            let log = this.logs[i];

            let found = all.some(x => x.ymd == log.ymd && x.id == log.id);

            if (!found)
            {
                let ex = lookup[log.id];

                let copy = new LogExercise({ ...ex, ...log });

                copy.sets = WorkoutSet.format(copy.sets.join(','));

                copy.updateOneRepMax();

                all.push(copy);
            }
        }

        this.logsRepository.save().then(() => this.cancel());
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
