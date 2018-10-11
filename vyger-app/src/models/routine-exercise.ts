import { Exercise } from './exercise';
import { utilities } from './utilities';

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
    }

    static compare(a: RoutineExercise, b: RoutineExercise): number
    {
        const seq = a.sequence - b.sequence;

        if (seq != 0)
        {
            return seq;
        }

        return Exercise.compare(a, b);
    }
}
