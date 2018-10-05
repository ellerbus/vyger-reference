import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Exercise } from '../../../models/exercise';
import { ExercisesRepository } from '../../exercises.repository';
import { PageTitleService } from '../../../page-title/page-title.service';
import { utilities } from '../../../models/utilities';

@Component({
    selector: 'app-exercise-edit',
    templateUrl: './exercise-edit.component.html',
    styleUrls: ['./exercise-edit.component.css']
})
export class ExerciseEditComponent implements OnInit
{
    exercise: Exercise;
    clone: Exercise;
    saving: boolean;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private exercisesRepository: ExercisesRepository) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Edit Exercise');

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.exercisesRepository
            .getExercise(id)
            .then(this.onloadingExercise);
    }

    private onloadingExercise = (exercise: Exercise): void =>
    {
        if (exercise == null)
        {
            this.router.navigateByUrl('/exercises');
        }
        else
        {
            this.exercise = exercise;

            this.clone = <Exercise>{ ...this.exercise };
        }
    }

    cancel(): void
    {
        this.router.navigateByUrl('/exercises');
    }

    save(): void
    {
        const keys = ['group', 'category', 'name'];

        utilities.extend(this.exercise, this.clone, keys);

        this.saving = true;

        this.exercisesRepository
            .save()
            .then(() =>
            {
                this.saving = false;
                this.router.navigateByUrl('/exercises');
            });
    }
}
