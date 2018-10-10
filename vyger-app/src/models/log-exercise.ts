import { Exercise } from './exercise';
import { utilities } from './utilities';
import { WorkoutSet } from './workout-set';

export class LogExercise extends Exercise
{
    private max: number;

    ymd: string;
    sets: string[];
    sequence: number = 1;

    maxset: number;

    constructor(source?: any)
    {
        super(source);

        const keys = ['ymd', 'sets', 'sequence'];

        utilities.extend(this, source, keys);
    }

    get oneRepMax(): number
    {
        if (this.max)
        {
            return this.max;
        }

        for (let i = 0; i < this.sets.length; i++)
        {
            const set = new WorkoutSet(this.sets[i]);

            if (this.max == null || this.max < set.oneRepMax)
            {
                this.maxset = i;
                this.max = set.oneRepMax;
            }
        }

        return this.max;
    }

    static compare(a: LogExercise, b: LogExercise): number
    {
        const ymd = a.ymd.localeCompare(b.ymd);

        if (ymd != 0)
        {
            return ymd;
        }

        const seq = a.sequence - b.sequence;

        if (seq != 0)
        {
            return seq;
        }

        return Exercise.compare(a, b);
    }
}
