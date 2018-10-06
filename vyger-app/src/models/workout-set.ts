export enum WorkoutSetTypes
{
    Static,
    Reference,
    RepMax
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
        this.parse(set);
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
            else if (word.match(/^[0-9]RM.*$/))
            {
                this.loadRepMax(word);
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

        if (items.length > 1 && items[1].match(/^-?[0-9]+(.[0-9]+)?%$/g))
        {
            let p = items[1].replace('-', '').replace('%', '');

            this.percent = (+p / 100.0);
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
            case WorkoutSetTypes.Static:
                patterns.push('' + this.weight);
                break;
            case WorkoutSetTypes.RepMax:
                patterns.push(this.repmax + 'RM');
                break;
        }

        if (this.type != WorkoutSetTypes.Static && this.percent > 0)
        {
            let p = (this.percent * 100).toFixed(2).replace('.00', '');

            patterns.push('-' + p + '%');
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
