import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Routine } from 'src/models/routine';
import { RoutineExercise } from 'src/models/routine-exercise';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';
import { RoutineService } from 'src/services/routine.service';

@Component({
    selector: 'app-routine-exercise-update',
    templateUrl: './routine-exercise-update.component.html'
})
export class RoutineExerciseUpdateComponent implements OnInit
{
    saving: boolean;
    routine: Routine;
    clones: RoutineExercise[];
    day: number;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private routineService: RoutineService)
    {
        this.day = 1;
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Routine Exercises');

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.day = +this.activatedRoute.snapshot.queryParamMap.get('day');

        this.loadTitle();

        this.loadExercises();

        this.updateBreadCrumbs();

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
            // const subtitle = 'clones week=' + this.week + ' day=' + this.day;

            // this.pageTitleService.setTitle(this.routine.name, subtitle);
        }
    }

    private loadExercises = (): void =>
    {
        if (this.routine)
        {
            let exercise = this.activatedRoute.snapshot.queryParamMap.get('exercise');

            this.clones = this.routine.exercises
                .filter(x => x.week <= this.routine.weeks && x.day == this.day && x.id == exercise)
                .sort(RoutineExercise.compare);
        }
    }

    cancel(): void
    {
        const queryParams = { week: 1, day: this.day };

        let url = this.router.createUrlTree(['/routines/exercises/', this.routine.id], { queryParams });

        this.router.navigateByUrl(url);
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
            .find(x => x.week == clone.week && x.day == this.day && x.id == clone.id);

        original.pattern = clone.pattern;
    };

    private updateBreadCrumbs = () =>
    {
        if (this.routine)
        {
            let filter = 'Day=' + this.day;

            this.breadCrumbService.clear();
            this.breadCrumbService.add('Home', '/');
            this.breadCrumbService.add('Routines', '/routines');
            this.breadCrumbService.add(this.routine.name, '/routines/exercises/' + this.routine.id);
            this.breadCrumbService.add('Update Exercise', null, filter);
        }
    };
}
