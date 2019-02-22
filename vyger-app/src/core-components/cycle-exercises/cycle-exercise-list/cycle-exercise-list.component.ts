import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Cycle } from 'src/models/cycle';
import { CycleExercise } from 'src/models/cycle-exercise';
import { utilities } from 'src/models/utilities';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { CycleService } from 'src/services/cycle.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';

class DatePair
{
    value: string;
    label: string;

    constructor(dt: Date)
    {
        let options = { weekday: 'short', month: 'short', day: 'numeric' };

        this.value = utilities.getYMD(dt);
        this.label = dt.toLocaleDateString('en-US', options);
    }
}

@Component({
    selector: 'app-cycle-exercise-list',
    templateUrl: './cycle-exercise-list.component.html'
})
export class CycleExerciseListComponent implements OnInit
{
    cycle: Cycle;
    exercises: CycleExercise[];
    week: number;
    day: number;
    canLogWorkout: boolean;
    dates: DatePair[];
    date: DatePair;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private cycleService: CycleService)
    {
        this.week = 1;
        this.day = 1;
        this.canLogWorkout = false;
        this.dates = [];

        let today = new Date();

        this.dates.push();

        for (let i = 1; i < 10; i++)
        {
            today.setDate(today.getDate() - i);

            this.dates.push(new DatePair(today));
        }

        this.date = this.dates[0];
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('CycleExercises');

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.activatedRoute.queryParams.subscribe((x: Params) =>
        {
            if (x.week)
            {
                this.week = +x.week;
            }
            if (x.day)
            {
                this.day = +x.day;
            }

            this.loadTitle();

            this.loadExercises();

            this.updateBreadCrumbs();
        });

        this.cycleService
            .getCycle(id)
            .then(this.onloadingCycle);
    }

    private onloadingCycle = (cycle: Cycle): void =>
    {
        if (cycle == null)
        {
            let msg = 'Selected cycle could not be found'

            this.flashMessageService.danger(msg, true);

            this.router.navigateByUrl('/cycles');
        }
        else
        {
            this.cycle = cycle;

            this.loadTitle();

            this.loadExercises();

            this.updateBreadCrumbs();
        }
    }

    private loadTitle = (): void =>
    {
        if (this.cycle)
        {
            const subtitle = 'clones week=' + this.week + ' day=' + this.day;

            this.pageTitleService.setTitle(this.cycle.name, subtitle);
        }
    }

    private loadExercises = (): void =>
    {
        if (this.cycle)
        {
            this.exercises = this.cycle.exercises
                .filter(x => x.week == this.week && x.day == this.day)
                .sort(CycleExercise.compare);
        }
    }

    private updateBreadCrumbs = () =>
    {
        if (this.cycle)
        {
            let filter = 'Week=' + this.week + ', Day=' + this.day;

            this.breadCrumbService.clear();
            this.breadCrumbService.add('Home', '/');
            this.breadCrumbService.add('Cycles', '/cycles');
            this.breadCrumbService.add('View Exercises', null, filter);
        }
    };
}
