import { Component, OnInit, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { WorkoutSet, WorkoutSetTypes } from 'src/models/workout-set';
import { LogExercise } from 'src/models/log-exercise';
import { utilities } from 'src/models/utilities';

@Component({
    selector: 'app-log-exercise-set',
    templateUrl: './log-exercise-set.component.html',
    styleUrls: ['./log-exercise-set.component.css']
})
export class LogExerciseSetComponent implements OnInit, OnChanges
{
    reps: number[];

    set: WorkoutSet;

    @Input() exercise: LogExercise;
    @Input() pattern: string;
    @Input() expanded: number;
    @Input() index: number;

    @Output() patternChange = new EventEmitter<string>();
    @Output() expandedChange = new EventEmitter<number>();

    constructor()
    {
    }

    ngOnChanges(change: SimpleChanges)
    {
        if (change.pattern)
        {
            if (change.pattern.currentValue && change.pattern.previousValue == null)
            {
                this.set = new WorkoutSet(this.pattern);
            }
        }
    }

    ngOnInit()
    {
        this.loadReps();
    }

    private loadReps = (): void =>
    {
        this.reps = [];
        for (let i = 0; i < utilities.reps; i++)
        {
            this.reps.push(i + 1);
        }
    }

    remove(): void
    {
        this.exercise.sets.splice(this.index, 1);
    }

    update(): void
    {
        this.patternChange.emit(this.set.pattern);
    }

    toggleExpanded(): void
    {
        if (this.expanded == this.index)
        {
            this.expanded = -1;
        }
        else
        {
            this.expanded = this.index;
        }

        this.expandedChange.emit(this.index);
    }
}
