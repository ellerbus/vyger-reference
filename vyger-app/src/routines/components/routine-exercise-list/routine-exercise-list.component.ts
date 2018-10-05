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
    week: number = 1;
    day: number = 1;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private routinesRepository: RoutinesRepository) { }

    ngOnInit()
    {
        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.activatedRoute.queryParams.subscribe((x: Params) =>
        {
            if (x.week)
            {
                this.week = + x.week;
            }
            if (x.day)
            {
                this.day = +x.day;
            }

            this.updateTitle();
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
        }
    }

    private updateTitle = (): void =>
    {
        const title = 'exercises week=' + this.week + ' day=' + this.day;

        this.pageTitleService.setSubTitle(title);
    }

    getExercises(): RoutineExercise[]
    {
        if (this.routine)
        {
            return this.routine.exercises
                .filter(x => x.week == this.week && x.day == this.day)
                .sort(RoutineExercise.compare);
        }

        return [];
    }
}
