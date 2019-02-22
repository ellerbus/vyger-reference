import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { LogExercise } from 'src/models/log-exercise';

@Component({
    selector: 'app-log-exercise-sets',
    templateUrl: './log-exercise-sets.component.html',
    styleUrls: ['./log-exercise-sets.component.css']
})
export class LogExerciseSetsComponent implements OnInit, OnChanges
{
    expanded: number;

    @Input() exercise: LogExercise;

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
        this.exercise.sets.push('100x5');

        this.expanded = this.exercise.sets.length - 1;
    }

    trackByIndex(index: number, item: string)
    {
        return index;
    }
}
