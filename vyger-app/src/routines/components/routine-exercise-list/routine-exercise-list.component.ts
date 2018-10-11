import { Component, OnInit } from '@angular/core';
import { Routine } from 'src/models/routine';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { PageTitleService } from 'src/page-title/page-title.service';
import { RoutineExercise } from 'src/models/routine-exercise';
import { RoutinesRepository } from '../../routines.repository';

@Component({
    selector: 'app-routine-exercise-list',
    templateUrl: './routine-exercise-list.component.html',
    styleUrls: ['./routine-exercise-list.component.css']
})
export class RoutineExerciseListComponent implements OnInit
{
    routine: Routine;
    exercises: RoutineExercise[];
    week: number;
    day: number;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private routinesRepository: RoutinesRepository)
    {
        this.week = 1;
        this.day = 1;
    }

    ngOnInit()
    {
        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.activatedRoute.queryParams.subscribe((x: Params) =>
        {
            if (x.week)
            {
                this.week = +x.week;
            }
            if (x.day)
            {
                this.day = +x.day;
            }

            this.updateTitle();

            this.updateExercises();
        });

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

            this.pageTitleService.setTitle(this.routine.name);

            this.updateTitle();

            this.updateExercises();
        }
    }

    private updateTitle = (): void =>
    {
        const title = 'exercises week=' + this.week + ' day=' + this.day;

        this.pageTitleService.setSubTitle(title);
    }

    private updateExercises = (): void =>
    {
        if (this.routine)
        {
            this.exercises = this.routine.exercises
                .filter(x => x.week == this.week && x.day == this.day)
                .sort(RoutineExercise.compare);
        }
    }

    //  crutch for sortablejs callback
    resequence = (): void =>
    {
        if (this.exercises)
        {
            const keys = this.getSequenceKeys();

            const sequencing = this.routine.exercises.filter(x => x.day == this.day);

            for (let i = 0; i < sequencing.length; i++)
            {
                let ex = sequencing[i];

                let seq = keys.indexOf(ex.id);

                ex.sequence = seq + 1;
            }

            this.routinesRepository.save();
        }
    }

    private getSequenceKeys = (): string[] =>
    {
        let keys = [];

        for (let i = 0; i < this.exercises.length; i++)
        {
            const ex = this.exercises[i];

            keys.push(ex.id);
        }

        return keys;
    }
}
