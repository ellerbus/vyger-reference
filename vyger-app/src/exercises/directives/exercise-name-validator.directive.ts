import { Directive, Input } from '@angular/core';
import { FormControl, ValidationErrors, NG_ASYNC_VALIDATORS, AsyncValidator } from '@angular/forms';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { Exercise } from '../models/exercise';
import { ExercisesService } from '../exercises.service';

@Directive({
    selector: '[uniqueExerciseName][ngModel]',
    providers: [
        { provide: NG_ASYNC_VALIDATORS, useExisting: ExerciseNameValidatorDirective, multi: true }
    ]
})
export class ExerciseNameValidatorDirective implements AsyncValidator {
    @Input('uniqueExerciseName') exercise: Exercise;

    constructor(
        private exercisesService: ExercisesService) { }

    validate(c: FormControl): Observable<ValidationErrors> {
        return this.exercisesService
            .getExercises()
            .map(exercises => {
                for (let i = 0; i < exercises.length; i++) {
                    let ex: Exercise = exercises[i];
                    if (this.exercise.id != ex.id) {
                        if (ex.matchesName(this.exercise.category, c.value)) {
                            return true;
                        }
                    }
                }

                return false;
            })
            .map(exists => exists ? { uniqueExerciseName: 'x' } : null);
    }
}
