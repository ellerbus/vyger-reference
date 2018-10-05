import { Exercise, IExercise } from './exercise';
import { utilities } from './utilities';

export interface IRoutineExercise extends IExercise
{
    week: number;
    day: number;
    sets?: string[];
}

export class RoutineExercise extends Exercise implements IRoutineExercise
{
    week: number;
    day: number;
    sequence: number = 1;
    sets: string[] = [];

    constructor(source?: IRoutineExercise)
    {
        super(source);

        const keys = ['week', 'day', 'sets'];

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
