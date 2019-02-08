import { Component, OnInit } from '@angular/core';
import { Cycle } from 'src/models/cycle';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { PageTitleService } from 'src/services/page-title.service';
import { CycleExercise } from 'src/models/cycle-exercise';
import { CyclesRepository } from '../../cycles.repository';

@Component({
    selector: 'app-cycle-exercise-list',
    templateUrl: './cycle-exercise-list.component.html',
    styleUrls: ['./cycle-exercise-list.component.css']
})
export class CycleExerciseListComponent implements OnInit
{
    cycle: Cycle;
    exercises: CycleExercise[];
    week: number;
    day: number;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private cyclesRepository: CyclesRepository)
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

        this.cyclesRepository
            .getCycle(id)
            .then(this.onloadingCycle);
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

            this.updateTitle();

            this.updateExercises();
        }
    }

    private updateTitle = (): void =>
    {
        if (this.cycle)
        {
            const subtitle = 'exercises week=' + this.week + ' day=' + this.day;

            this.pageTitleService.setTitle(this.cycle.name, subtitle);
        }
    }

    private updateExercises = (): void =>
    {
        if (this.cycle)
        {
            this.exercises = this.cycle.exercises
                .filter(x => x.week == this.week && x.day == this.day)
                .sort(CycleExercise.compare);
        }
    }
}
