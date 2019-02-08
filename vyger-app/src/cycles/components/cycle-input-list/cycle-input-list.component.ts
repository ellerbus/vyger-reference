import { Component, OnInit } from '@angular/core';
import { Cycle } from 'src/models/cycle';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { PageTitleService } from 'src/services/page-title.service';
import { CycleService } from 'src/services/cycle.service';
import { utilities } from 'src/models/utilities';
import { ExerciseLogService } from 'src/services/exercise-log.service';
import { LogExercise } from 'src/models/log-exercise';
import { WorkoutSet } from 'src/models/workout-set';
import { CycleInput } from 'src/models/cycle-input';
import { CycleGenerator } from 'src/models/cycle-generator';

@Component({
    selector: 'app-cycle-input-list',
    templateUrl: './cycle-input-list.component.html',
    styleUrls: ['./cycle-input-list.component.css']
})
export class CycleInputListComponent implements OnInit
{
    saving: boolean;
    cycle: Cycle;
    inputs: CycleInput[];
    reps: number[];
    percents: number[];

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private CycleService: CycleService,
        private logRepository: ExerciseLogService)
    {
    }

    ngOnInit()
    {
        this.loadReps();
        this.loadPercents();

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.CycleService
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
    }

    private onloadingCycle = (cycle: Cycle): void =>
    {
        if (cycle == null)
        {
            this.router.navigateByUrl('/cycles');
        }
        else if (cycle.lastLogged != null)
        {
            let [week, day] = cycle.lastLogged.split(':');

            const queryParams = { week: week, day: day };

            let url = this.router.createUrlTree(['/cycles/exercises/' + cycle.id], { queryParams });

            this.router.navigateByUrl(url);
        }
        else
        {
            this.cycle = cycle;

            this.pageTitleService.setTitle(this.cycle.name, 'cycle inputs');

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
                    let set = new WorkoutSet(max.sets[max.oneRepMaxSet]);

                    input.weight = set.weight;
                    input.reps = set.reps;
                }
            }
        }

        this.inputs = this.cycle.inputs
            .filter(x => x.requiresInput)
            .sort(CycleInput.compare);
    }

    cancel()
    {
        this.router.navigateByUrl('/cycles');
    }

    save()
    {
        this.saving = true;

        let cg = new CycleGenerator(this.cycle);

        cg.generate();

        this.CycleService
            .save()
            .then(() =>
            {
                this.router.navigateByUrl('/cycles/exercises/' + this.cycle.id);
            });
    }
}
