import { Component, Input } from '@angular/core';
import { Routine } from '../../../models/routine';
import { utilities } from 'src/models/utilities';

@Component({
    selector: 'app-routine-days',
    templateUrl: './routine-days.component.html',
    styleUrls: ['./routine-days.component.css']
})
export class RoutineDaysComponent
{
    @Input() routine: Routine;

    totalDays: number[];

    constructor()
    {
        this.totalDays = [];

        for (let i = 0; i < utilities.days; i++)
        {
            this.totalDays.push(i + 1);
        }
    }
}
