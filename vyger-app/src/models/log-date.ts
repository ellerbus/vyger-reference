import { utilities } from './utilities';

const days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

export class LogDay
{
    full: string;
    short: string;
    ymd: string;
    active: boolean;

    constructor(public index: number, public date: Date)
    {
        this.full = days[index];
        this.short = this.full[0];
        this.ymd = utilities.getYMD(this.date);
    }
}

export class LogDate
{
    date: Date;
    week: string;
    day: number;
    days: LogDay[];

    constructor(dt: Date)
    {
        this.date = dt;

        this.day = this.date.getDay();

        let sunday = utilities.getStartOfWeek(this.date);

        this.week = utilities.getYMD(sunday);

        this.days = [];

        for (let i = 0; i < 7; i++)
        {
            let tmp = new Date(sunday);

            tmp.setDate(sunday.getDate() + i);

            let d = new LogDay(i, tmp);

            d.active = i == this.day;

            this.days.push(d);
        }
    }
}
