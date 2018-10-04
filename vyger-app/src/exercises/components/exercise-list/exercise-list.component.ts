import { Component, OnInit } from '@angular/core';
import { PageTitleService } from 'src/page-title/page-title.service';
import { Exercise } from '../../models/exercise';

import { ExercisesService } from '../../exercises.service';

@Component({
    selector: 'app-exercise-list',
    templateUrl: './exercise-list.component.html',
    styleUrls: ['./exercise-list.component.css']
})
export class ExerciseListComponent implements OnInit {
    private exercises: Exercise[];

    constructor(
        private pageTitleService: PageTitleService,
        private exercisesService: ExercisesService) { }

    ngOnInit() {
        this.pageTitleService.setTitle('Exercises');

        this.exercisesService
            .getExercises()
            .subscribe(this.onloadingExercises);
    }

    private onloadingExercises = (data: Exercise[]) => {
        this.exercises = data.sort(Exercise.compare);
    };

    getExercises(): Exercise[] {
        if (this.exercises) {
            return this.exercises;
        }

        return [];
    }
}
