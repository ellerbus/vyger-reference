import { utilities } from './utilities';

export class CalendarDay
{
    display: string;
    active: boolean;

    constructor(
        public date: string,
        public current: boolean)
    {
        let [y, m, d] = date.split('-');

        this.display = d;
    }
}

export class CalendarWeek
{
    days: CalendarDay[];

    constructor()
    {
        this.days = [];
    }
}

export class CalendarMonth
{
    weeks: CalendarWeek[];
    days: CalendarDay[];
    month: Date;
    start: string;
    end: string;

    constructor(private date: Date)
    {
        this.month = new Date(date);

        this.start = utilities.getStartOfCalendarMonth(this.date);

        this.date = utilities.toDate(this.start);

        this.weeks = [];

        this.days = [];

        for (let w = 0; w < 5; w++)
        {
            this.loadWeek(w);
        }
    }

    private loadWeek = (w: number): void =>
    {
        let week = new CalendarWeek();

        for (let d = 0; d < 7; d++)
        {
            this.end = utilities.getYMD(this.date);

            let current = this.date.getMonth() == this.month.getMonth();

            let day = new CalendarDay(this.end, current);

            week.days.push(day);

            this.days.push(day);

            this.date.setDate(this.date.getDate() + 1);
        }

        this.weeks.push(week);
    }

}
