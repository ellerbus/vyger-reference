import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { WorkoutSet } from 'src/models/workout-set';
import { Routine } from 'src/models/routine';
import { RoutineExercise } from 'src/models/routine-exercise';

@Component({
    selector: 'app-routine-exercise-set',
    templateUrl: './routine-exercise-set.component.html',
    styleUrls: ['./routine-exercise-set.component.css']
})
export class RoutineExerciseSetComponent implements OnInit, OnChanges
{
    set: WorkoutSet;

    @Input() pattern: string;
    @Input() routine: Routine;
    @Input() exercise: RoutineExercise;

    constructor() { }

    ngOnChanges(change: SimpleChanges)
    {
        if (change.pattern.currentValue != change.pattern.previousValue)
        {
            this.set = new WorkoutSet(this.pattern);
        }
    }

    ngOnInit()
    {
    }

    add(): void
    {
        this.exercise.sets.push(this.set.pattern);
    }
}
