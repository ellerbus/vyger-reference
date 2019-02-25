import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Cycle } from 'src/models/cycle';
import { CycleGenerator } from 'src/models/cycle-generator';
import { CycleInput } from 'src/models/cycle-input';
import { ExerciseLog } from 'src/models/exercise-log';
import { utilities } from 'src/models/utilities';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { CycleService } from 'src/services/cycle.service';
import { ExerciseLogService } from 'src/services/exercise-log.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';

@Component({
    selector: 'app-cycle-input-list',
    templateUrl: './cycle-input-list.component.html'
})
export class CycleInputListComponent implements OnInit
{
    saving: boolean;

    id: string;
    cycle: Cycle;
    previous: Cycle;
    clones: CycleInput[];
    exercises: ExerciseLog[];

    reps: number[];
    percents: number[];

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private exerciseLogRepository: ExerciseLogService,
        private cycleService: CycleService)
    {
        this.loadReps();
        this.loadPercents();
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Cycle Inputs');

        this.id = this.activatedRoute.snapshot.paramMap.get('id');

        let cycleLoad = this.cycleService
            .getCycles()
            .then(this.onloadingCycles);

        let exericseLoad = this.exerciseLogRepository
            .getLogs()
            .then(this.onloadingExercises);

        Promise
            .all([cycleLoad, exericseLoad])
            .then(this.checkForInputs);

        this.updateBreadCrumbs();
    }

    private onloadingExercises = (exercises: ExerciseLog[]): void =>
    {
        this.exercises = exercises;
    }

    private onloadingCycles = (cycles: Cycle[]): void =>
    {
        this.cycle = cycles.find(x => x.id == this.id);

        if (this.cycle == null)
        {
            let msg = 'Selected cycle could not be found'

            this.flashMessageService.danger(msg, true);

            this.router.navigateByUrl('/cycles');
        }
        else
        {
            this.previous = cycles.find(x => x.sequence == this.cycle.sequence - 1);

            if (this.previous == null)
            {
                this.previous = new Cycle();

                this.previous.inputs = [];
            }
        }
    }

    private checkForInputs = (): void =>
    {
        if (this.cycle.inputs == null || this.cycle.inputs.length == 0)
        {
            this.cycle.inputs = this.findUniqueInputs();

            this.overlayInputs();
        }

        this.clones = this.cycle.inputs
            .filter(x => x.requiresInput)
            .map(x => new CycleInput(x));
    }

    private findUniqueInputs = (): CycleInput[] =>
    {
        let picked = [];

        let inputs = [];

        for (let i = 0; i < this.cycle.exercises.length; i++)
        {
            let e = this.cycle.exercises[i];

            if (picked.indexOf(e.id) == -1)
            {
                let requiresInput = e.sets.join(',').indexOf('RM') > -1;

                inputs.push(new CycleInput({ ...e, requiresInput }));

                picked.push(e.id);
            }
        }

        return inputs;
    }

    private overlayInputs = (): void =>
    {
        let [then, now] = this.getLookbackDates();

        for (let i = 0; i < this.cycle.inputs.length; i++)
        {
            let input = this.cycle.inputs[i];

            let [recent, max] = this.getExercise(input.id, then, now);

            let goal = this.previous.inputs.find(x => x.id == input.id);

            if (recent != null && goal != null)
            {
                //  was our most recent above or below our goal
                if (recent.oneRepMax > goal.oneRepMax)
                {
                    let s = recent.getMaxWorkoutSet();

                    input.weight = s.weight;
                    input.reps = s.reps;
                    input.pullback = -3;

                    continue;
                }
            }

            if (max != null && recent != null)
            {
                //  was our recent within 10% of our max
                if (recent.oneRepMax > max.oneRepMax * 0.90)
                {
                    let s = recent.getMaxWorkoutSet();

                    input.weight = s.weight;
                    input.reps = s.reps;
                    input.pullback = -3;

                    continue;
                }
            }

            if (max != null)
            {
                let s = recent.getMaxWorkoutSet();

                input.weight = s.weight;
                input.reps = s.reps;

                //  max over 90 days pullback 10%
                let days = utilities.diffdays(max.ymd, now);

                if (days > 90)
                {
                    input.pullback = 10;
                }

                continue;
            }
        }
    }

    private getLookbackDates = (): string[] =>
    {
        let dt = new Date();

        let now = utilities.getYMD(dt);

        dt.setDate(dt.getDate() - (this.cycle.weeks * 7));

        let then = utilities.getYMD(dt);

        return [then, now];
    }

    private getExercise(id: string, then: string, now: string): ExerciseLog[]
    {
        let recent = null;

        let max = null;

        for (let i = 0; i < this.exercises.length; i++)
        {
            let log = this.exercises[i];

            if (log.id == id)
            {
                if (max == null || log.oneRepMax > max.oneRepMax)
                {
                    max = log;
                }

                if (recent == null)
                {
                    recent = log;
                }

                if (log.ymd >= then && log.ymd <= now)
                {
                    if (log.oneRepMax > recent.oneRepMax)
                    {
                        recent = log;
                    }
                }
            }
        }

        return [recent, max];
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
        this.percents = [-3];

        for (let x = 0; x <= 50; x += 5)
        {
            this.percents.push(x);
        }
    }

    cancel(): void
    {
        this.router.navigateByUrl('/cycles');
    }

    save(): void
    {
        this.saving = true;

        if (this.cycle.lastLogged == null)
        {
            for (let i = 0; i < this.clones.length; i++)
            {
                this.overlayOntoInput(this.clones[i]);
            }

            let generator = new CycleGenerator(this.cycle);

            generator.generate();
        }

        this.cycleService
            .save()
            .then(this.onsavedInputs);
    }

    private onsavedInputs = (): void =>
    {
        this.saving = false;

        this.flashMessageService.success('Inputs Saved Successfully', true);

        this.router.navigateByUrl('/cycles/exercises/' + this.cycle.id);
    };

    private overlayOntoInput = (clone: CycleInput): void =>
    {
        let original = this.cycle
            .inputs
            .find(x => x.id == clone.id);

        original.weight = clone.weight;
        original.reps = clone.reps;
        original.pullback = clone.pullback;
    };

    private updateBreadCrumbs = () =>
    {
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Cycles', '/cycles');
        this.breadCrumbService.add('Inputs');
    };
}
