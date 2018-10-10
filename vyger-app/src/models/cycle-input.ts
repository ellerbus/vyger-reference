import { Exercise } from './exercise';
import { utilities } from './utilities';

export class CycleInput extends Exercise
{
    weight: number;
    reps: number = 5;
    pullback: number = 0;

    constructor(source?: any)
    {
        super(source);

        const keys = ['weight', 'reps', 'pullback'];

        utilities.extend(this, source, keys);
    }

    get oneRepMax(): number
    {
        return utilities.oneRepMax(this.weight, this.reps);
    }

    static compare(a: CycleInput, b: CycleInput): number
    {
        return Exercise.compare(a, b);
    }
}
