import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Exercise } from 'src/models/exercise';
import { utilities } from 'src/models/utilities';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { ExerciseService } from 'src/services/exercise.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';

@Component({
    selector: 'app-exercise-update',
    templateUrl: './exercise-update.component.html'
})
export class ExerciseUpdateComponent implements OnInit
{
    original: Exercise;
    clone: Exercise;
    saving: boolean;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private exerciseService: ExerciseService) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Update Exercise');

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.exerciseService
            .getExercise(id)
            .then(this.onloadingExercise);

        this.updateBreadCrumbs();
    }

    private onloadingExercise = (exercise: Exercise): void =>
    {
        if (exercise == null)
        {
            let msg = 'Selected exercise could not be found'

            this.flashMessageService.danger(msg, true);

            this.router.navigateByUrl('/exercises');
        }
        else
        {
            this.original = exercise;

            this.clone = new Exercise({ ...this.original });
        }
    }

    cancel(): void
    {
        this.router.navigateByUrl('/exercises');
    }

    save(): void
    {
        const keys = ['properties'];

        utilities.extend(this.original, this.clone, keys);

        this.saving = true;

        this.exerciseService
            .save()
            .then(() =>
            {
                this.saving = false;
                this.router.navigateByUrl('/exercises');
            });
    }

    private updateBreadCrumbs = () =>
    {
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Exercises', '/Exercises');
        this.breadCrumbService.add('Update Exercise');
    };
}
