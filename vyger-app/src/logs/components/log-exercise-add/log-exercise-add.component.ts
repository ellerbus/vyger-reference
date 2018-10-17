import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { PageTitleService } from 'src/page-title/page-title.service';
import { LogExercise } from 'src/models/log-exercise';
import { Exercise } from 'src/models/exercise';
import { ExercisesRepository } from 'src/exercises/exercises.repository';
import { LogsRepository } from '../../logs.repository';
import { utilities } from 'src/models/utilities';

@Component({
    selector: 'app-log-exercise-add',
    templateUrl: './log-exercise-add.component.html',
    styleUrls: ['./log-exercise-add.component.css']
})
export class LogExerciseAddComponent implements OnInit
{
    exercise: LogExercise;
    saving: boolean;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private logsRepository: LogsRepository,
        private exercisesRepository: ExercisesRepository)
    {
        this.exercise = new LogExercise();
        this.exercise.id = null;
        this.exercise.group = null;
        this.exercise.category = null;
        this.exercise.ymd = utilities.getYMD(new Date());
        this.exercise.sets = [];
    }

    ngOnInit()
    {
        this.exercise.ymd = this.activatedRoute.snapshot.queryParamMap.get('date');

        this.pageTitleService.setTitle(this.exercise.ymd, 'adding exercises');
    }

    cancel(): void
    {
        const queryParams = { date: this.exercise.ymd };

        let url = this.router.createUrlTree(['/logs/'], { queryParams });

        this.router.navigateByUrl(url);
    }

    save(another: boolean)
    {
        this.saving = true;

        this.exercisesRepository
            .getExercise(this.exercise.id)
            .then(this.onsavingLogExercise)
            .then(() =>
            {
                this.cancel();
            });
    }

    private onsavingLogExercise = (exercise: Exercise): Promise<any> =>
    {
        const clone = {
            ...this.exercise,
            ...exercise
        };

        let ex = new LogExercise(clone);

        ex.updateOneRepMax();

        return this.logsRepository.add(ex);
    }
}

