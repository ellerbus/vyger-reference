import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { Exercise } from 'src/models/exercise';
import { Router } from '@angular/router';
import { PageTitleService } from 'src/services/page-title.service';
import { LogsRepository } from 'src/logs/logs.repository';
import { ExercisesRepository } from 'src/exercises/exercises.repository';
import { LogExercise } from 'src/models/log-exercise';

@Component({
    selector: 'app-log-history',
    templateUrl: './log-history.component.html',
    styleUrls: ['./log-history.component.css']
})
export class LogHistoryComponent implements OnInit, OnChanges
{
    logs: LogExercise[];
    history: LogExercise[];
    exercise: LogExercise;

    constructor(
        private router: Router,
        private pageTitleService: PageTitleService,
        private logsRepository: LogsRepository,
        private exercisesRepository: ExercisesRepository)
    {
        this.history = [];

        this.exercise = new LogExercise();
        this.exercise.id = null;
        this.exercise.group = null;
        this.exercise.category = null;
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Log History', 'by exercise');

        this.logsRepository
            .getLogs()
            .then(this.onloadingLogs);
    }

    private onloadingLogs = (data: LogExercise[]) =>
    {
        this.logs = data;
    }

    ngOnChanges(changes: SimpleChanges): void
    {
        console.log(changes);
    }

    getHistory()
    {
        this.history = this.logs
            .filter(x => x.id == this.exercise.id)
            .sort(LogExercise.compare)
            .reverse();
    }
}
