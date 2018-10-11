import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Exercise } from 'src/models/exercise';
import { PageTitleService } from 'src/page-title/page-title.service';
import { ExercisesRepository } from '../../exercises.repository';

@Component({
    selector: 'app-exercise-add',
    templateUrl: './exercise-add.component.html',
    styleUrls: ['./exercise-add.component.css']
})
export class ExerciseAddComponent implements OnInit
{
    exercise: Exercise;
    saving: boolean;

    constructor(
        private router: Router,
        private pageTitleService: PageTitleService,
        private exercisesRepository: ExercisesRepository) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Add Exercise');

        this.exercise = new Exercise();
        this.exercise.group = null;
        this.exercise.category = null;
    }

    cancel(): void
    {
        this.router.navigateByUrl('/exercises');
    }

    save(): void
    {
        this.saving = true;

        this.exercisesRepository
            .add(this.exercise)
            .then(() =>
            {
                this.saving = false;
                this.router.navigateByUrl('/exercises');
            });
    }
}
