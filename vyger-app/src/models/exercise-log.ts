import { Exercise } from './exercise';
import { utilities } from './utilities';
import { WorkoutSet } from './workout-set';

export class ExerciseLog extends Exercise
{
    ymd: string;
    sets: string[] = [];
    sequence: number = 1;

    constructor(source?: any)
    {
        super(source);

        const keys = ['ymd', 'sequence'];

        utilities.extend(this, source, keys);

        if (source && source.sets)
        {
            this.sets = [...source.sets];
        }
    }

    get pattern(): string
    {
        return this.sets.join(', ');
    }

    set pattern(value: string)
    {
        this.sets = WorkoutSet.format(value);
    }

    get oneRepMax(): number
    {
        let set = this.getMaxWorkoutSet();

        if (set == null)
        {
            return 0;
        }

        return set.oneRepMax;
    }

    set oneRepMax(value: number) { }

    getMaxWorkoutSet(): WorkoutSet
    {
        let max: WorkoutSet = null;

        for (let i = 0; i < this.sets.length; i++)
        {
            let set = new WorkoutSet(this.sets[i]);

            if (max == null || max.oneRepMax < set.oneRepMax)
            {
                max = set;
            }
        }

        return max;
    }

    static compare(a: ExerciseLog, b: ExerciseLog): number
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
