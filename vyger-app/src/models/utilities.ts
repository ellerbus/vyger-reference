export namespace utilities
{
    export const cycles = 8;
    export const weeks = 8;
    export const days = 7;
    export const reps = 12;
    export const repmaxes = 8;

    export function generateId(prefix: string, length: number): string
    {
        const possible = '0123456789abcdefghijklmnopqrstuvwxyz';
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

    export function getStartOfMonth(date: Date): string
    {
        let first = new Date(date.getFullYear(), date.getMonth(), 1);

        return getYMD(first);
    }

    export function getStartOfWeek(date: Date): Date
    {
        let dt = new Date(date);

        const diff = dt.getDate() - dt.getDay();

        dt.setDate(diff);

        const [y, m, d] = [...getYMD(dt).split('-')];

        const sunday = new Date(+y, +m - 1, +d);

        return sunday;
    }

    export function getYMD(date: Date): string
    {
        let dt = new Date(date);

        const y = '' + dt.getFullYear();
        const m = ('' + (dt.getMonth() + 1)).padStart(2, '0');
        const d = ('' + dt.getDate()).padStart(2, '0');

        return [y, m, d].join('-');
    }

    export function toDate(ymd: string): Date
    {
        const [y, m, d] = [...ymd.split('-')];

        let dt = new Date(+y, +m - 1, +d);

        return dt;
    }
}
