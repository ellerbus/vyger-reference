import { Component, OnInit, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { WorkoutSet, WorkoutSetTypes } from 'src/models/workout-set';
import { Routine } from 'src/models/routine';
import { RoutineExercise } from 'src/models/routine-exercise';
import { utilities } from 'src/models/utilities';

@Component({
    selector: 'app-routine-exercise-set',
    templateUrl: './routine-exercise-set.component.html',
    styleUrls: ['./routine-exercise-set.component.css']
})
export class RoutineExerciseSetComponent implements OnInit, OnChanges
{
    private types: string[];
    reps: number[];
    repmaxes: number[];
    percents: number[];

    set: WorkoutSet;

    @Input() exercise: RoutineExercise;
    @Input() pattern: string;
    @Input() expanded: number;
    @Input() first: boolean;
    @Input() last: boolean;
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
        this.loadTypes();
        this.loadReps();
        this.loadRepMaxes();
        this.loadPercents();
    }

    private loadReps = (): void =>
    {
        this.reps = [];
        for (let i = 0; i < utilities.reps; i++)
        {
            this.reps.push(i + 1);
        }
    }

    private loadTypes = (): void =>
    {
        this.types = [];
        for (let x in WorkoutSetTypes)
        {
            this.types.push(x);
        }
    }

    private loadRepMaxes = (): void =>
    {
        this.repmaxes = [];
        for (let i = 0; i < utilities.repmaxes; i++)
        {
            this.repmaxes.push(i + 1);
        }
    }

    private loadPercents = (): void =>
    {
        this.percents = [];
        for (let x = 50; x <= 100; x += 5)
        {
            this.percents.push(x);
        }
        this.percents.push(102.5);
    }

    getTypes(): string[]
    {
        let types = [];

        for (let i = 0; i < this.types.length; i++)
        {
            if (this.types[i] == WorkoutSetTypes.Reference)
            {
                if (!this.last)
                {
                    types.push(this.types[i]);
                }
            }
            else
            {
                types.push(this.types[i]);
            }
        }

        return types;
    }

    getReferences(): { key: string, value: string }[]
    {
        let references = [];

        references.push({ key: 'L', value: 'last set' });

        // for (let i = 0; i < this.exercise.sets.length; i++)
        // {
        //     references.push({ key: (i + 1).toString(), value: 'set #' + (i + 1) });
        // }

        return references;
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
