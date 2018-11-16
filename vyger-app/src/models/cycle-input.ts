import { Exercise } from './exercise';
import { utilities } from './utilities';

export class CycleInput extends Exercise
{
    weight: number;
    reps: number = 5;
    pullback: number = 0;
    requiresInput: boolean;

    constructor(source?: any)
    {
        super(source);

        const keys = ['weight', 'reps', 'pullback', 'requiresInput'];

        utilities.extend(this, source, keys);
    }

    get oneRepMax(): number
    {
        let orm = utilities.oneRepMax(this.weight, this.reps);

        if (this.pullback != 0)
        {
            orm = orm * (1.0 - (this.pullback / 100.0));
        }

        return orm;
    }

    static compare(a: CycleInput, b: CycleInput): number
    {
        return Exercise.compare(a, b);
    }
}
