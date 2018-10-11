import { Component, OnInit } from '@angular/core';
import { Cycle } from 'src/models/cycle';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { PageTitleService } from 'src/page-title/page-title.service';
import { CyclesRepository } from '../../cycles.repository';
import { utilities } from 'src/models/utilities';
import { LogsRepository } from 'src/logs/logs.repository';
import { LogExercise } from 'src/models/log-exercise';
import { WorkoutSet } from 'src/models/workout-set';

@Component({
    selector: 'app-cycle-input-list',
    templateUrl: './cycle-input-list.component.html',
    styleUrls: ['./cycle-input-list.component.css']
})
export class CycleInputListComponent implements OnInit
{
    saving: boolean;
    cycle: Cycle;
    reps: number[];
    percents: number[];

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private cyclesRepository: CyclesRepository,
        private logRepository: LogsRepository)
    {
    }

    ngOnInit()
    {
        this.loadReps();
        this.loadPercents();

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.cyclesRepository
            .getCycle(id)
            .then(this.onloadingCycle);
    }

    private loadReps = (): void =>
    {
        this.reps = [];
        for (let i = 0; i < utilities.reps; i++)
        {
            this.reps.push(i + 1);
        }
    }

    private loadPercents = (): void =>
    {
        this.percents = [];
        for (let x = 0; x <= 50; x += 5)
        {
            this.percents.push(x);
        }
        this.percents.push(102.5);
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

            this.pageTitleService.setSubTitle('cycle inputs');

            this.logRepository
                .getMaxes()
                .then(this.onloadingInputs);
        }
    }

    private onloadingInputs = (data: { [key: string]: LogExercise }) =>
    {
        for (let i = 0; i < this.cycle.inputs.length; i++)
        {
            let input = this.cycle.inputs[i];

            if (input.weight == null)
            {
                let max = data[input.id];

                if (max)
                {
                    const set = new WorkoutSet(max.sets[max.oneRepMaxSet]);

                    input.weight = set.weight;
                    input.reps = set.reps;
                }
            }
        }
    }

    cancel()
    {
        this.router.navigateByUrl('/cycles');
    }

    save()
    {
        this.saving = true;

        this.cyclesRepository
            .save()
            .then(() =>
            {
                this.router.navigateByUrl('/cycles/exercises/' + this.cycle.id);
            });
    }
}
