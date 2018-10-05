import { Component, OnInit, Input, SimpleChange, SimpleChanges, OnChanges } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { Routine } from 'src/models/routine';

@Component({
    selector: 'app-routine-days-picker',
    templateUrl: './routine-exercise-days-picker.component.html',
    styleUrls: ['./routine-exercise-days-picker.component.css']
})
export class RoutineExerciseDaysPickerComponent implements OnInit, OnChanges
{
    week: number = 1;
    day: number = 1;
    days: number[];

    @Input() routine: Routine;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute) { }

    ngOnChanges(change: SimpleChanges)
    {
        if (change.routine.currentValue && change.routine.previousValue == null)
        {
            this.days = [];

            for (let i = 0; i < this.routine.days; i++)
            {
                this.days.push(i + 1);
            }
        }
    }

    ngOnInit()
    {
        this.activatedRoute.queryParams.subscribe((x: Params) =>
        {
            if (x.week)
            {
                this.week = + x.week;
            }
            if (x.day)
            {
                this.day = +x.day;
            }
        });
    }

    updateParam(): void
    {
        const queryParams = { week: this.week, day: this.day };

        let url = this.router.createUrlTree(['/routines/exercises/', this.routine.id], { queryParams });

        this.router.navigateByUrl(url);
    }
}
