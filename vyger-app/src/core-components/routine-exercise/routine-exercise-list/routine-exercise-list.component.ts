import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Routine } from 'src/models/routine';
import { RoutineExercise } from 'src/models/routine-exercise';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';
import { RoutineService } from 'src/services/routine.service';

@Component({
    selector: 'app-routine-exercise-list',
    templateUrl: './routine-exercise-list.component.html'
})
export class RoutineExerciseListComponent implements OnInit
{
    saving: boolean;
    routine: Routine;
    clones: RoutineExercise[];
    week: number;
    day: number;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private routineService: RoutineService)
    {
        this.week = 1;
        this.day = 1;
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('RoutineExercises');

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

            this.loadTitle();

            this.loadExercises();

            this.updateBreadCrumbs();
        });

        this.routineService
            .getRoutine(id)
            .then(this.onloadingRoutine);
    }

    private onloadingRoutine = (routine: Routine): void =>
    {
        if (routine == null)
        {
            let msg = 'Selected routine could not be found'

            this.flashMessageService.danger(msg, true);

            this.router.navigateByUrl('/routines');
        }
        else
        {
            this.routine = routine;

            this.loadTitle();

            this.loadExercises();

            this.updateBreadCrumbs();
        }
    }

    private loadTitle = (): void =>
    {
        if (this.routine)
        {
            const subtitle = 'clones week=' + this.week + ' day=' + this.day;

            this.pageTitleService.setTitle(this.routine.name, subtitle);
        }
    }

    private loadExercises = (): void =>
    {
        if (this.routine)
        {
            this.clones = this.routine.exercises
                .filter(x => x.week == this.week && x.day == this.day)
                .sort(RoutineExercise.compare);
        }
    }

    save(): void
    {
        this.saving = true;

        for (let i = 0; i < this.clones.length; i++)
        {
            this.overlayOntoExercise(this.clones[i]);
        }

        this.routineService
            .save()
            .then(this.onsavedExercises);
    }

    private onsavedExercises = (): void =>
    {
        this.saving = false;

        this.flashMessageService.success('Saved Successfully', true);
    };

    private overlayOntoExercise = (clone: RoutineExercise): void =>
    {
        let original = this.routine
            .exercises
            .find(x => x.week == this.week && x.day == this.day && x.id == clone.id);

        original.pattern = clone.pattern;
        original.sequence = clone.sequence;
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
        if (this.routine)
        {
            let filter = 'Week=' + this.week + ', Day=' + this.day;

            this.breadCrumbService.clear();
            this.breadCrumbService.add('Home', '/');
            this.breadCrumbService.add('Routines', '/routines');
            this.breadCrumbService.add(this.routine.name, '/routines/update/' + this.routine.id);
            this.breadCrumbService.add('Update Exercises', null, filter);
        }
    };
}
