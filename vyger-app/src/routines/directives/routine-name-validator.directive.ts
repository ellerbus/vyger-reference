import { Directive, Input } from '@angular/core';
import { FormControl, ValidationErrors, NG_ASYNC_VALIDATORS, AsyncValidator } from '@angular/forms';
import { Observable, from } from 'rxjs';
import 'rxjs/add/operator/map';

import { Routine } from '../../models/routine';
import { RoutinesRepository } from '../routines.repository';

@Directive({
    selector: '[uniqueRoutineName][ngModel]',
    providers: [
        { provide: NG_ASYNC_VALIDATORS, useExisting: RoutineNameValidatorDirective, multi: true }
    ]
})
export class RoutineNameValidatorDirective implements AsyncValidator {
    @Input('uniqueRoutineName') routine: Routine;

    constructor(
        private routinesRepository: RoutinesRepository) { }

    validate(c: FormControl): Observable<ValidationErrors> {
        let p = this.routinesRepository
            .getRoutines()
            .then(routines => {
                for (let i = 0; i < routines.length; i++) {
                    let r: Routine = routines[i];
                    if (this.routine.id != r.id) {
                        if (Routine.matches(r, c.value)) {
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
