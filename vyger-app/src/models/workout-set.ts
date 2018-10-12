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
        this.reference = 'L';
        this.weight = 0;
        this.repmax = 1;
        this.percent = 100;
        this.reps = 1;
        this.repeat = 1;

        this.parse(set);
    }

    get oneRepMax(): number
    {
        return utilities.oneRepMax(this.weight, this.reps);
    }

    private parse = (set: string): void =>
    {
        const parts = set.split(/[xX/]/gi);

        this.loadWeight(parts);
        this.loadReps(parts);
        this.loadSets(parts);
    }

    private loadWeight = (parts: string[]): void =>
    {
        if (parts.length > 0)
        {
            const word = parts[0];

            if (word.match(/^[0-9]+$/))
            {
                this.loadStaticWeight(word);
            }
            else if (word.match(/^BW$/))
            {
                this.type = WorkoutSetTypes.BodyWeight;
            }
            else if (word.match(/^[0-9]RM.*$/))
            {
                this.loadRepMax(word);
            }
            else if (word.match(/^\[[0-9L]\].*$/))
            {
                this.loadReference(word);
            }
        }
    }

    private loadStaticWeight = (word: string): void =>
    {
        this.type = WorkoutSetTypes.Static;

        this.weight = +word;
    }

    private loadRepMax = (word: string): void =>
    {
        this.type = WorkoutSetTypes.RepMax;

        const items = word.split(/RM/gi);

        this.repmax = +items[0];

        if (items.length > 1)
        {
            this.loadPercent(items[1]);
        }
    }

    private loadReference = (word: string): void =>
    {
        this.type = WorkoutSetTypes.Reference;

        const pos = word.indexOf(']');

        this.reference = word.substr(1, pos - 1);

        if (word.length > pos + 1)
        {
            this.loadPercent(word.substr(pos + 1));
        }
    }

    private loadPercent = (word: string): void =>
    {
        if (word.match(/^-?[0-9]+(.[0-9]+)?%$/g))
        {
            let p = word.replace('-', '').replace('%', '');

            this.percent = +p;
        }
    }

    private loadReps = (parts: string[]): void =>
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

    private loadSets = (parts: string[]): void =>
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

    get pattern(): string
    {
        let patterns: string[] = [];

        switch (this.type)
        {
            case WorkoutSetTypes.BodyWeight:
                patterns.push('BW');
                break;
            case WorkoutSetTypes.Static:
                patterns.push('' + this.weight);
                break;
            case WorkoutSetTypes.RepMax:
                patterns.push(this.repmax + 'RM');
                break;
            case WorkoutSetTypes.Reference:
                patterns.push('[' + this.reference + ']');
                break;
        }

        switch (this.type)
        {
            case WorkoutSetTypes.RepMax:
            case WorkoutSetTypes.Reference:
                if (this.percent > 0)
                {
                    let p = (this.percent * 1.0).toFixed(2).replace('.00', '');

                    if (p != '100')
                    {
                        patterns.push('-' + p + '%');
                    }
                }
                break;
        }

        patterns.push('x' + this.reps);

        if (this.repeat > 1)
        {
            patterns.push('x' + this.repeat);
        }

        return patterns.join('');
    }

    set pattern(value: string)
    {
        this.parse(value);
    }
}
