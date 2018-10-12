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

        const keys = ['week', 'day', 'sequence'];

        utilities.extend(this, source, keys);

        if (source.sets)
        {
            this.sets = [...source.sets];
        }

        if (source.plan)
        {
            this.plan = [...source.plan];
        }
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
