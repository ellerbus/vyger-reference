import { Component, OnInit } from '@angular/core';
import { PageTitleService } from 'src/page-title/page-title.service';
import { Exercise } from '../../models/exercise';

import { ExercisesService } from '../../exercises.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-exercises',
    templateUrl: './exercises.component.html',
    styleUrls: ['./exercises.component.css']
})
export class ExercisesComponent implements OnInit {
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
