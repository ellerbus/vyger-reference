import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Routine } from 'src/models/routine';


@Component({
    selector: 'app-routine-weeks-picker',
    templateUrl: './routine-exercise-weeks-picker.component.html'
})
export class RoutineExerciseWeeksPickerComponent implements OnInit, OnChanges
{
    day: number = 1;
    week: number = 1;
    weeks: number[];

    @Input() routine: Routine;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute) { }

    ngOnChanges(change: SimpleChanges)
    {
        if (change.routine.currentValue && change.routine.previousValue == null)
        {
            this.weeks = [];

            for (let i = 0; i < this.routine.weeks; i++)
            {
                this.weeks.push(i + 1);
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
