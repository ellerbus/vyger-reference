import { Component, Input } from '@angular/core';
import { Routine } from '../../../models/routine';
import { utilities } from 'src/models/utilities';

@Component({
    selector: 'app-routine-weeks',
    templateUrl: './routine-weeks.component.html',
    styleUrls: ['./routine-weeks.component.css']
})
export class RoutineWeeksComponent
{
    @Input() routine: Routine;

    totalWeeks: number[];

    constructor()
    {
        this.totalWeeks = [];

        for (let i = 0; i < utilities.weeks; i++)
        {
            this.totalWeeks.push(i + 1);
        }
    }
}
