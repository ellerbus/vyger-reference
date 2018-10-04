import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Exercise, Categories } from '../../models/exercise';
import { PageTitleService } from 'src/page-title/page-title.service';
import { ExercisesService } from '../../exercises.service';

@Component({
    selector: 'app-exercise-add',
    templateUrl: './exercise-add.component.html',
    styleUrls: ['./exercise-add.component.css']
})
export class ExerciseAddComponent implements OnInit {
    categories: string[];
    exercise: Exercise;

    constructor(
        private router: Router,
        private pageTitleService: PageTitleService,
        private exercisesService: ExercisesService) { }

    ngOnInit() {
        this.pageTitleService.setTitle('Add Exercise');
        this.loadExercise();
    }

    loadExercise(): void {
        this.exercise = new Exercise();

        this.exercise.group = null;
        this.exercise.category = null;
    }

    cancel(): void {
        this.router.navigateByUrl('/exercises');
    }

    save(): void {

    }
}
