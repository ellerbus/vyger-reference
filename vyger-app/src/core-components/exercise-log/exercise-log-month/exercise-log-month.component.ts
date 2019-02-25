import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { CalendarMonth } from 'src/models/calendar.model';
import { ExerciseLog } from 'src/models/exercise-log';
import { utilities } from 'src/models/utilities';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { ExerciseLogService } from 'src/services/exercise-log.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { PageTitleService } from 'src/services/page-title.service';

@Component({
    selector: 'app-exercise-log-month',
    templateUrl: './exercise-log-month.component.html'
})
export class ExerciseLogMonthComponent implements OnInit
{
    calendar: CalendarMonth;
    date: string;
    exercises: ExerciseLog[];

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private flashMessageService: FlashMessageService,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private exerciseLogService: ExerciseLogService)
    {
        this.date = utilities.getYMD(new Date());
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Exercise Logs');

        this.activatedRoute.queryParams.subscribe((x: Params) =>
        {
            if (x.date)
            {
                this.date = x.date;
            }

            this.calendar = null;

            this.updateBreadCrumbs();

            this.exerciseLogService
                .getLogs()
                .then(this.onloadingExercises);
        });
    }

    private onloadingExercises = (exercises: ExerciseLog[]): void =>
    {
        this.calendar = new CalendarMonth(utilities.toDate(this.date));

        let start = this.calendar.start;
        let end = this.calendar.end;

        this.exercises = exercises.filter(x => x.ymd >= start && x.ymd <= end);

        for (let i = 0; i < this.exercises.length; i++)
        {
            let ymd = this.exercises[i].ymd;

            let day = this.calendar.days.find(x => x.date == ymd);

            if (day != null)
            {
                day.active = true;
            }
        }
    }

    goto(date: string)
    {
        this.router.navigateByUrl('/logs/exercises/' + date);
    }

    prev()
    {
        let d = utilities.toDate(this.date);

        d.setDate(1);

        d.setMonth(d.getMonth() - 1);

        let p = utilities.getYMD(d);

        let queryParams = { date: p };

        let url = this.router.createUrlTree(['/logs'], { queryParams });

        this.router.navigateByUrl(url);
    }

    next()
    {
        let d = utilities.toDate(this.date);

        d.setDate(1);

        d.setMonth(d.getMonth() + 1);

        let n = utilities.getYMD(d);

        let queryParams = { date: n };

        let url = this.router.createUrlTree(['/logs'], { queryParams });

        this.router.navigateByUrl(url);
    }

    private updateBreadCrumbs = () =>
    {
        let options = { month: 'long', year: 'numeric' };

        let value = utilities.toDate(this.date);

        let filter = value.toLocaleDateString('en-US', options);

        this.breadCrumbService.clear();
        this.breadCrumbService.add('Home', '/');
        this.breadCrumbService.add('Logs', null, filter);
    };
}
