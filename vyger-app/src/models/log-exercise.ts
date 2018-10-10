import { Exercise, IExercise } from './exercise';
import { utilities } from './utilities';

export interface ILogExercise extends IExercise
{
    ymd: string;
    sets?: string[];
    oneRepMax: number;
}

export class LogExercise extends Exercise implements ILogExercise
{
    ymd: string;
    sets: string[];
    oneRepMax: number;
    sequence: number = 1;

    constructor(source?: ILogExercise)
    {
        super(source);

        const keys = ['ymd', 'sets', 'oneRepMax', 'sequence'];

        utilities.extend(this, source, keys);
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
