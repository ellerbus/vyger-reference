import { Exercise } from './exercise';
import { utilities } from './utilities';
import { WorkoutSet } from './workout-set';

export class RoutineExercise extends Exercise
{
    week: number;
    day: number;
    sequence: number = 1;
    sets: string[] = [];

    constructor(source?: any)
    {
        super(source);

        const keys = ['week', 'day', 'sequence', 'sets'];

        utilities.extend(this, source, keys);

        if (this.sets)
        {
            let x = this.sets.join(', ');

            this.sets = WorkoutSet.format(x);
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

    static compare(a: RoutineExercise, b: RoutineExercise): number
    {
        const week = a.week - b.week;

        if (week != 0)
        {
            return week;
        }

        const seq = a.sequence - b.sequence;

        if (seq != 0)
        {
            return seq;
        }

        return Exercise.compare(a, b);
    }
}
