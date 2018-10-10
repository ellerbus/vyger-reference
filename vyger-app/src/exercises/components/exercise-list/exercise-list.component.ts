import { Component, OnInit } from '@angular/core';
import { PageTitleService } from 'src/page-title/page-title.service';
import { Exercise } from 'src/models/exercise';

import { ExercisesRepository } from '../../exercises.repository';

@Component({
    selector: 'app-exercise-list',
    templateUrl: './exercise-list.component.html',
    styleUrls: ['./exercise-list.component.css']
})
export class ExerciseListComponent implements OnInit
{
    exercises: Exercise[];

    constructor(
        private pageTitleService: PageTitleService,
        private exercisesRepository: ExercisesRepository) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Exercises');

        this.exercisesRepository
            .getExercises()
            .then(this.onloadingExercises);
    }

    private onloadingExercises = (data: Exercise[]) =>
    {
        this.exercises = data.sort(Exercise.compare);
    };
}
