import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Cycle } from 'src/models/cycle';
import { CycleExercise } from 'src/models/cycle-exercise';
import { ExerciseLog } from 'src/models/exercise-log';
import { utilities } from 'src/models/utilities';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { CycleService } from 'src/services/cycle.service';
import { ExerciseLogService } from 'src/services/exercise-log.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';

@Component({
    selector: 'app-exercise-log-cycle',
    templateUrl: './exercise-log-cycle.component.html'
})
export class ExerciseLogCycleComponent implements OnInit
{
    saving: boolean;
    date: string;
    exercises: ExerciseLog[];
    clones: ExerciseLog[];
    cycle: Cycle;
    week: number;
    day: number;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private exerciseLogService: ExerciseLogService,
        private cycleService: CycleService)
    {
        this.date = utilities.getYMD(new Date());
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Exercise Logs');

        this.date = this.activatedRoute.snapshot.paramMap.get('date');

        const id = this.activatedRoute.snapshot.queryParamMap.get('cycle');

        let loadCycle = this.cycleService
            .getCycle(id)
            .then(this.onloadingCycle);

        let loadLog = this.exerciseLogService
            .getLogs()
            .then(this.onloadingExercises);

        Promise.all([loadCycle, loadLog]).then(this.onloadingClones);

        this.updateBreadCrumbs();
    }

    private onloadingCycle = (cycle: Cycle): void =>
    {
        if (cycle == null)
        {
            let msg = 'Selected cycle could not be found'

            this.flashMessageService.danger(msg, true);

            this.router.navigateByUrl('/logs/exercises/' + this.date);
        }
        else
        {
            this.cycle = cycle;
        }
    }

    private onloadingExercises = (exercises: ExerciseLog[]): void =>
    {
        this.exercises = exercises;

        if (this.exercises)
        {
            this.clones = this.exercises
                .filter(x => x.ymd == this.date)
                .sort(ExerciseLog.compare);
        }
        else
        {
            this.clones = [];
        }
    }

    private onloadingClones = (): void =>
    {
        if (this.cycle && this.clones)
        {
            this.week = +this.activatedRoute.snapshot.queryParamMap.get('week');
            this.day = +this.activatedRoute.snapshot.queryParamMap.get('day');

            let exercises = this.cycle.exercises
                .filter(x => x.week == this.week && x.day == this.day)
                .sort(CycleExercise.compare);

            for (let i = 0; i < exercises.length; i++)
            {
                let logit = exercises[i];
                let clone = this.clones.find(x => x.id == logit.id);

                if (clone == null)
                {
                    clone = new ExerciseLog({ ...logit, ymd: this.date });

                    clone.pattern = logit.pattern;

                    this.clones.push(clone);
                }
            }
        }
    }

    save(): void
    {
        this.saving = true;

        for (let i = 0; i < this.clones.length; i++)
        {
            this.overlayOntoExercise(this.clones[i]);
        }

        this.exerciseLogService
            .save()
            .then(this.onsavedExercises);
    }

    private onsavedExercises = (): void =>
    {
        this.saving = false;

        this.flashMessageService.success('Saved Successfully', true);

        this.cycle.lastLogged = this.week + ':' + this.day;

        this.cycleService
            .save()
            .then(this.onsavedCycle);
    };

    private onsavedCycle = () =>
    {
        this.router.navigateByUrl('/logs/exercises/' + this.date);
    };

    private overlayOntoExercise = (clone: ExerciseLog): void =>
    {
        let original = this.exercises
            .find(x => x.ymd == this.date && x.id == clone.id);

        if (original == null)
        {
            original = new ExerciseLog({ ...clone });

            this.exercises.push(original);
        }

        original.pattern = clone.pattern;
    };

    //  syntacic crutch for sortablejs callback
    resequence = (): void =>
    {
        if (this.clones)
        {
            const keys = this.getSequenceKeys();

            for (let i = 0; i < this.clones.length; i++)
            {
                let ex = this.clones[i];

                let seq = keys.indexOf(ex.id);

                ex.sequence = seq + 1;
            }
        }
    }

    private getSequenceKeys = (): string[] =>
    {
        let keys = [];

        for (let i = 0; i < this.clones.length; i++)
        {
            const ex = this.clones[i];

            keys.push(ex.id);
        }

        return keys;
    }

    private updateBreadCrumbs = () =>
    {
        let filter = 'Date=' + this.date;

        this.breadCrumbService.clear();
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Logs', '/logs');
        this.breadCrumbService.add('Log Cycle', null, filter);
    };
}
