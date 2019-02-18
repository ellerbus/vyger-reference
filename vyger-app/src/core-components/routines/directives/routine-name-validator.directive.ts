import { Directive, Input } from '@angular/core';
import { AsyncValidator, FormControl, NG_ASYNC_VALIDATORS, ValidationErrors } from '@angular/forms';
import { from, Observable } from 'rxjs';
import { Routine } from 'src/models/routine';
import { RoutineService } from 'src/services/routine.service';

@Directive({
    selector: '[uniqueRoutineName][ngModel]',
    providers: [
        { provide: NG_ASYNC_VALIDATORS, useExisting: RoutineNameValidatorDirective, multi: true }
    ]
})
export class RoutineNameValidatorDirective implements AsyncValidator
{
    @Input('uniqueRoutineName') routine: Routine;

    constructor(
        private RoutineService: RoutineService) { }

    validate(c: FormControl): Observable<ValidationErrors>
    {
        let p = this.RoutineService
            .getRoutines()
            .then(routines =>
            {
                for (let i = 0; i < routines.length; i++)
                {
                    let r: Routine = routines[i];
                    if (this.routine.id != r.id)
                    {
                        if (Routine.matches(r, c.value))
                        {
                            return true;
                        }
                    }
                }

                return false;
            })
            .then(exists => exists ? { uniqueRoutineName: 'x' } : null);

        return from(p);
    }
}
