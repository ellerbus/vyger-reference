import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Cycle } from 'src/models/cycle';


@Component({
    selector: 'app-cycle-days-picker',
    templateUrl: './cycle-exercise-days-picker.component.html'
})
export class CycleExerciseDaysPickerComponent implements OnInit, OnChanges
{
    week: number = 1;
    day: number = 1;
    days: number[];

    @Input() cycle: Cycle;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute) { }

    ngOnChanges(change: SimpleChanges)
    {
        if (change.cycle.currentValue && change.cycle.previousValue == null)
        {
            this.days = [];

            for (let i = 0; i < this.cycle.days; i++)
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

        let url = this.router.createUrlTree(['/cycles/exercises/', this.cycle.id], { queryParams });

        this.router.navigateByUrl(url);
    }
}
