import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Exercise } from '../../models/exercise';
import { ExercisesRepository } from '../../exercises.repository';
import { PageTitleService } from '../../../page-title/page-title.service';

@Component({
    selector: 'app-exercise-edit',
    templateUrl: './exercise-edit.component.html',
    styleUrls: ['./exercise-edit.component.css']
})
export class ExerciseEditComponent implements OnInit {
    exercise: Exercise;
    clone: Exercise;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private exercisesRepository: ExercisesRepository) { }

    ngOnInit() {
        this.pageTitleService.setTitle('Edit Exercise');

        this.exercisesRepository
            .getExercises()
            .then(this.onloadingExercise);
    }

    private onloadingExercise = (exercises: Exercise[]): void => {
        const id = this.activatedRoute.snapshot.paramMap.get('id');

        for (let i = 0; i < exercises.length; i++) {
            if (exercises[i].id == id) {
                this.exercise = exercises[i];
                break;
            }
        }

        if (this.exercise == null) {
            this.router.navigateByUrl('/exercises');
        } else {
            this.clone = { ...this.exercise };
        }
    }

    cancel(): void {
        this.router.navigateByUrl('/exercises');
    }

    save(): void {

    }
}
