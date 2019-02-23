export namespace utilities
{
    export let cycles = 8;
    export let weeks = 8;
    export let days = 7;
    export let reps = 12;
    export let repmaxes = 8;

    export function generateId(prefix: string, length: number): string
    {
        let possible = '0123456789abcdefghijklmnopqrstuvwxyz';
        let id = [];

        for (let i = 0; i < length; i++)
        {
            id.push(possible.charAt(Math.floor(Math.random() * possible.length)));
        }

        return prefix + id.join('');
    }

    export function extend(target: any, source: any, keys: string[]): void
    {
        if (source)
        {
            for (let i = 0; i < keys.length; i++)
            {
                if (source[keys[i]])
                {
                    target[keys[i]] = source[keys[i]];
                }
            }
        }
    }

    export function getStartOfCalendarMonth(date: Date): string
    {
        let month = getStartOfMonth(date);

        return getStartOfWeek(toDate(month));
    }

    export function getStartOfMonth(date: Date): string
    {
        let first = new Date(date.getFullYear(), date.getMonth(), 1);

        return getYMD(first);
    }

    export function getStartOfWeek(date: Date): string
    {
        let dt = new Date(date);

        let diff = dt.getDate() - dt.getDay();

        dt.setDate(diff);

        let [y, m, d] = getYMD(dt).split('-');

        let sunday = new Date(+y, +m - 1, +d);

        return getYMD(sunday);
    }

    export function getYMD(date: Date): string
    {
        let dt = new Date(date);

        let y = '' + dt.getFullYear();
        let m = ('' + (dt.getMonth() + 1)).padStart(2, '0');
        let d = ('' + dt.getDate()).padStart(2, '0');

        return [y, m, d].join('-');
    }

    export function addDays(date: Date, days: number): Date
    {
        let dt = new Date(date);

        dt.setDate(dt.getDate() + days);

        return dt;
    }

    export function toDate(ymd: string): Date
    {
        let [y, m, d] = [...ymd.split('-')];

        let dt = new Date(+y, +m - 1, +d);

        return dt;
    }

    export function round(weight: number, increment: number = 5): number
    {
        return Math.round(weight / increment) * increment;
    }

    /// <remarks>https://www.exrx.net/Calculators/OneRepMax</remarks>
    export function oneRepMax(weight: number, reps: number): number
    {
        return weight / (1.0278 - 0.0278 * reps);
    }

    /// <remarks>https://www.exrx.net/Calculators/OneRepMax</remarks>
    export function prediction(oneRepMax: number, reps: number): number
    {
        return oneRepMax * (1.0278 - 0.0278 * reps);
    }

    export function diffdays(then: string, now: string): number
    {
        let thenDt = toDate(then);
        let nowDt = toDate(now);

        let diff = nowDt.valueOf() - thenDt.valueOf();

        return Math.round(diff / (1000 * 60 * 60 * 24));
    }
}
