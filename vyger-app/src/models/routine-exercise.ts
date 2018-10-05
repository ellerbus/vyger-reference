import { Exercise, IExercise } from './exercise';
import { utilities } from './utilities';

export interface IRoutineExercise extends IExercise
{
    week: number;
    day: number;
    sets: string[];
}

export class RoutineExercise extends Exercise implements IRoutineExercise
{
    week: number;
    day: number;
    sets: string[] = [];

    constructor(source: IRoutineExercise)
    {
        super(source);

        const keys = ['week', 'day', 'sets'];

        utilities.extend(this, source, keys);
    }
}
