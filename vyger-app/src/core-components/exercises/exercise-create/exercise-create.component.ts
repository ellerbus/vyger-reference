import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Exercise } from 'src/models/exercise';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { ExerciseService } from 'src/services/exercise.service';
import { PageTitleService } from 'src/services/page-title.service';


@Component({
    selector: 'app-exercise-create',
    templateUrl: './exercise-create.component.html'
})
export class ExerciseCreateComponent implements OnInit
{
    exercise: Exercise;
    saving: boolean;

    constructor(
        private router: Router,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private ExerciseService: ExerciseService) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Create Exercise');

        this.exercise = new Exercise();
        this.exercise.group = null;
        this.exercise.category = null;

        this.updateBreadCrumbs();
    }

    cancel(): void
    {
        this.router.navigateByUrl('/exercises');
    }

    save(): void
    {
        this.saving = true;

        this.ExerciseService
            .addExercise(this.exercise)
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
        this.breadCrumbService.add('Create Exercise');
    };
}