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

    get containsRepMax(): boolean
    {
        for (let i = 0; i < this.sets.length; i++)
        {
            if (this.sets[i].indexOf('RM') > -1)
            {
                return true;
            }
        }

        return false;
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
