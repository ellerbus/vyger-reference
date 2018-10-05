export namespace utilities
{
    export const weeks = 8;
    export const days = 7;
    export const reps = 12;
    export const cycles = 8;

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
                target[keys[i]] = source[keys[i]];
            }
        }
    }
}
