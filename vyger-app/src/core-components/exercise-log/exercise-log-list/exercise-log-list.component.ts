import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExerciseLog } from 'src/models/exercise-log';
import { utilities } from 'src/models/utilities';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { ExerciseLogService } from 'src/services/exercise-log.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';

@Component({
    selector: 'app-exercise-log-list',
    templateUrl: './exercise-log-list.component.html'
})
export class ExerciseLogListComponent implements OnInit
{
    saving: boolean;
    date: string;
    exercises: ExerciseLog[];
    clones: ExerciseLog[];

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private exerciseLogService: ExerciseLogService)
    {
        this.date = utilities.getYMD(new Date());
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Exercise Logs');

        this.date = this.activatedRoute.snapshot.paramMap.get('date');

        this.updateBreadCrumbs();

        this.exerciseLogService
            .getLogs()
            .then(this.onloadingExercises);
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
    };

    private overlayOntoExercise = (clone: ExerciseLog): void =>
    {
        let original = this.exercises
            .find(x => x.ymd == this.date && x.id == clone.id);

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
        this.breadCrumbService.add('Update Exercises', null, filter);
    };
}
