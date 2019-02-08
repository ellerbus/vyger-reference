import { Component, OnInit } from '@angular/core';
import { PageTitleService } from 'src/services/page-title.service';
import { Exercise } from 'src/models/exercise';

import { ExerciseService } from 'src/services/exercise.service';

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
        private ExerciseService: ExerciseService) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Exercises');

        this.ExerciseService
            .getExercises()
            .then(this.onloadingExercises);
    }

    private onloadingExercises = (data: Exercise[]) =>
    {
        this.exercises = data.sort(Exercise.compare);
    };
}
