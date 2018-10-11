import { Exercise } from './exercise';
import { utilities } from './utilities';

export class CycleExercise extends Exercise
{
    week: number;
    day: number;
    sequence: number = 1;
    sets: string[] = [];
    plan: string[] = [];

    constructor(source?: any)
    {
        super(source);

        const keys = ['week', 'day', 'sets', 'plan'];

        utilities.extend(this, source, keys);
    }

    static compare(a: CycleExercise, b: CycleExercise): number
    {
        const seq = a.sequence - b.sequence;

        if (seq != 0)
        {
            return seq;
        }

        return Exercise.compare(a, b);
    }
}
