import { Directive, Input } from '@angular/core';
import { AsyncValidator, FormControl, NG_ASYNC_VALIDATORS, ValidationErrors } from '@angular/forms';
import { from, Observable } from 'rxjs';
import { Exercise } from 'src/models/exercise';
import { ExerciseService } from 'src/services/exercise.service';

@Directive({
    selector: '[uniqueExerciseName][ngModel]',
    providers: [
        { provide: NG_ASYNC_VALIDATORS, useExisting: ExerciseNameValidatorDirective, multi: true }
    ]
})
export class ExerciseNameValidatorDirective implements AsyncValidator
{
    @Input('uniqueExerciseName') exercise: Exercise;

    constructor(
        private ExerciseService: ExerciseService) { }

    validate(c: FormControl): Observable<ValidationErrors>
    {
        let p = this.ExerciseService
            .getExercises()
            .then(exercises =>
            {
                for (let i = 0; i < exercises.length; i++)
                {
                    let ex: Exercise = exercises[i];
                    if (this.exercise.id != ex.id)
                    {
                        if (Exercise.matches(ex, this.exercise.category, c.value))
                        {
                            return true;
                        }
                    }
                }

                return false;
            })
            .then(exists => exists ? { uniqueExerciseName: 'x' } : null);

        return from(p);
    }
}
