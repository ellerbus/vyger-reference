import { Component, OnInit } from '@angular/core';
import { Cycle } from 'src/models/cycle';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { PageTitleService } from 'src/page-title/page-title.service';
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

            this.pageTitleService.setTitle(this.cycle.name);

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
        if (this.cycle)
        {
            this.exercises = this.cycle.exercises
                .filter(x => x.week == this.week && x.day == this.day)
                .sort(CycleExercise.compare);
        }
    }

    //  crutch for sortablejs callback
    resequence = (): void =>
    {
        if (this.exercises)
        {
            const keys = this.getSequenceKeys();

            const sequencing = this.cycle.exercises.filter(x => x.day == this.day);

            for (let i = 0; i < sequencing.length; i++)
            {
                const ex = sequencing[i];

                const seq = keys.indexOf(ex.id);

                ex.sequence = seq + 1;
            }

            this.cyclesRepository.save();
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
