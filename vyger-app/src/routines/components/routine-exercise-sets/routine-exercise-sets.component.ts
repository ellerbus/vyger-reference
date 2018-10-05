import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { Routine } from 'src/models/routine';
import { RoutineExercise } from 'src/models/routine-exercise';
import { WorkoutSet } from 'src/models/workout-set';

@Component({
    selector: 'app-routine-exercise-sets',
    templateUrl: './routine-exercise-sets.component.html',
    styleUrls: ['./routine-exercise-sets.component.css']
})
export class RoutineExerciseSetsComponent implements OnInit, OnChanges
{
    sets: WorkoutSet[];

    @Input() routine: Routine;
    @Input() exercise: RoutineExercise;

    constructor() { }

    ngOnChanges(change: SimpleChanges)
    {
        if (change.exercise.currentValue && change.exercise.previousValue == null)
        {
            this.sets = this.exercise.sets.map(x => new WorkoutSet(x));
        }
    }

    ngOnInit()
    {
    }

}
