import { Component, Input } from '@angular/core';
import { Routine } from 'src/models/routine';
import { utilities } from 'src/models/utilities';
import { ControlContainer, NgForm } from '@angular/forms';

@Component({
    selector: 'app-routine-weeks',
    templateUrl: './routine-weeks.component.html',
    styleUrls: ['./routine-weeks.component.css'],
    viewProviders: [
        { provide: ControlContainer, useExisting: NgForm }
    ]
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
