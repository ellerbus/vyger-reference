import { Exercise } from './exercise';
import { utilities } from './utilities';
import { WorkoutSet } from './workout-set';

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

    plannedOneRepMax(): number
    {
        let oneRepMax = null;

        for (let i = 0; i < this.sets.length; i++)
        {
            let set = new WorkoutSet(this.sets[i]);

            if (oneRepMax == null || oneRepMax < set.oneRepMax)
            {
                oneRepMax = set.oneRepMax;
            }
        }

        return oneRepMax;
    }

    get pattern(): string
    {
        return this.plan.join(', ');
    }

    set pattern(value: string)
    {
        this.plan = WorkoutSet.format(value);
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
