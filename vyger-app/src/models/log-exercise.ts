import { Exercise } from './exercise';
import { utilities } from './utilities';
import { WorkoutSet } from './workout-set';

export class LogExercise extends Exercise
{
    ymd: string;
    sets: string[];
    sequence: number = 1;

    oneRepMaxSet: number;
    oneRepMax: number;

    constructor(source?: any)
    {
        super(source);

        const keys = ['ymd', 'sets', 'sequence', 'oneRepMaxSet', 'oneRepMax'];

        utilities.extend(this, source, keys);

        this.oneRepMaxSet = this.oneRepMaxSet || 0;
    }

    updateOneRepMax()
    {
        this.oneRepMax = null;

        for (let i = 0; i < this.sets.length; i++)
        {
            const set = new WorkoutSet(this.sets[i]);

            if (this.oneRepMax == null || this.oneRepMax < set.oneRepMax)
            {
                this.oneRepMaxSet = i;
                this.oneRepMax = set.oneRepMax;
            }
        }
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
