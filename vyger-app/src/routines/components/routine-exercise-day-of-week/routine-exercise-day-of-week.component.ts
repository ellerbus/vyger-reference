import { Component, Input } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';

import { Routine } from 'src/models/routine';
import { RoutineExercise } from 'src/models/routine-exercise';

@Component({
    selector: 'app-routine-exercise-day-of-week',
    templateUrl: './routine-exercise-day-of-week.component.html',
    styleUrls: ['./routine-exercise-day-of-week.component.css'],
    viewProviders: [
        { provide: ControlContainer, useExisting: NgForm }
    ]
})
export class RoutineExerciseDayOfWeekComponent
{
    @Input() routine: Routine;
    @Input() exercise: RoutineExercise;

    getDaysOfWeek(): number[]
    {
        let days = [];

        for (let i = 0; i < this.routine.days; i++)
        {
            days.push(i + 1);
        }

        return days;
    }
}
