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
    expanded: number;

    @Input() routine: Routine;
    @Input() exercise: RoutineExercise;

    constructor()
    {
        this.expanded = 0;
    }

    ngOnChanges(change: SimpleChanges)
    {
    }

    ngOnInit()
    {
    }

    add()
    {
        this.exercise.sets.push('5RM-90%x5');
    }

    trackByIndex(index: number, item: string)
    {
        return index;
    }
}
