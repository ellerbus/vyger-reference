import { utilities } from "./utilities";

export enum WorkoutSetTypes
{
    BodyWeight = 'BodyWeight',
    Static = 'Static',
    RepMax = 'RepMax',
    Reference = 'Reference'
}

export class WorkoutSet
{
    type: WorkoutSetTypes;
    reference: string;
    weight: number;
    repmax: number;
    percent: number;
    reps: number;
    repeat: number;

    constructor(public set: string)
    {
        this.type = WorkoutSetTypes.Static;
        this.reference = 'L';
        this.weight = 0;
        this.repmax = 1;
        this.percent = 100;
        this.reps = 1;
        this.repeat = 1;

        this.parse((set || '').toUpperCase());
    }

    get oneRepMax(): number
    {
        return utilities.oneRepMax(this.weight, this.reps);
    }

    private parse = (set: string): void =>
    {
        let parts = set.split(/[xX/]/gi);

        this.parseWeight(parts);
        this.parseReps(parts);
        this.parseRepeat(parts);
    }

    private parseWeight = (parts: string[]): void =>
    {
        if (parts.length > 0)
        {
            let word = parts[0];

            if (word.match(/^[0-9]+$/))
            {
                this.parseStaticWeight(word);
            }
            else if (word.match(/^BW$/))
            {
                this.type = WorkoutSetTypes.BodyWeight;
            }
            else if (word.match(/^[0-9]RM.*$/))
            {
                this.parseRepMax(word);
            }
            else if (word.match(/^[=#]?[Ll].*$/))
            {
                this.parseReferenceUsingAssignment(word);
            }
            else if (word.match(/^\[[0-9L]\].*$/))
            {
                this.parseReferenceUsingBrackets(word);
            }
        }
    }

    private parseStaticWeight = (word: string): void =>
    {
        this.type = WorkoutSetTypes.Static;

        this.weight = +word;
    }

    private parseRepMax = (word: string): void =>
    {
        this.type = WorkoutSetTypes.RepMax;

        let items = word.split(/RM/gi);

        this.repmax = +items[0];

        if (items.length > 1)
        {
            this.parsePercent(items[1]);
        }
    }

    private parseReferenceUsingAssignment = (word: string): void =>
    {
        this.type = WorkoutSetTypes.Reference;

        this.reference = word.substr(1, 1);

        if (word.length > 2)
        {
            let pos = word.indexOf('-');

            if (pos > -1)
            {
                this.parsePercent(word.substr(pos));
            }
            else
            {
                this.parsePercent(word.substr(2));
            }
        }
    }

    private parseReferenceUsingBrackets = (word: string): void =>
    {
        this.type = WorkoutSetTypes.Reference;

        let pos = word.indexOf(']');

        this.reference = word.substr(1, pos - 1);

        if (word.length > pos + 1)
        {
            this.parsePercent(word.substr(pos + 1));
        }
    }

    private parsePercent = (word: string): void =>
    {
        if (word.match(/^-?[0-9]+(.[0-9]+)?%$/g))
        {
            let p = word.replace('-', '').replace('%', '');

            this.percent = +p;
        }
    }

    private parseReps = (parts: string[]): void =>
    {
        if (parts.length > 1)
        {
            this.reps = +parts[1];
        }
        else
        {
            this.reps = 1;
        }
    }

    private parseRepeat = (parts: string[]): void =>
    {
        if (parts.length > 2)
        {
            this.repeat = +parts[2];
        }
        else
        {
            this.repeat = 1;
        }
    }

    formatWeight(): string
    {
        switch (this.type)
        {
            case WorkoutSetTypes.BodyWeight:
                return 'BW';
            case WorkoutSetTypes.Static:
                return '' + this.weight;
            case WorkoutSetTypes.RepMax:
                return this.repmax + 'RM';
            case WorkoutSetTypes.Reference:
                return '=' + this.reference;
        }

        throw new Error('Unknown Format for ' + this.type);
    }

    formatPercent(): string
    {
        switch (this.type)
        {
            case WorkoutSetTypes.RepMax:
            case WorkoutSetTypes.Reference:
                if (this.percent > 0)
                {
                    let p = (this.percent * 1.0).toFixed(2).replace('.00', '');

                    if (p != '100')
                    {
                        return '-' + p + '%';
                    }
                }
                break;
        }

        return '';
    }

    formatReps(): string
    {
        return 'x' + this.reps;
    }

    formatRepeat(): string
    {
        if (this.repeat > 1)
        {
            return 'x' + this.repeat;
        }

        return '';
    }

    get pattern(): string
    {
        let pattern = this.formatWeight()
            + this.formatPercent()
            + this.formatReps()
            + this.formatRepeat();

        return pattern;
    }

    set pattern(value: string)
    {
        this.parse(value);
    }

    static format(pattern: string): string[]
    {
        return pattern.split(/, */).map(x => new WorkoutSet(x).pattern);
    }
}
