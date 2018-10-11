import { utilities } from 'src/models/utilities';
import { RoutineExercise } from './routine-exercise';

export class Routine
{
    id: string = utilities.generateId('r', 2);
    name: string;
    weeks: number = 4;
    days: number = 3;
    exercises: RoutineExercise[] = [];
    sets: string[] = [];

    constructor(source?: any)
    {
        const keys = ['id', 'name', 'weeks', 'days', 'sets'];

        utilities.extend(this, source, keys);

        if (source && source.exercises)
        {
            this.exercises = source.exercises.map(x => new RoutineExercise(x));
        }
    }

    static matches(r: Routine, name: string): boolean
    {
        if (r.name != null && name != null)
        {
            return r.name.toLowerCase() == name.toLowerCase();
        }

        return false;
    }

    static defaultList(): Routine[]
    {
        return [
            new Routine({ name: 'Sample' }),
        ];
    }

    static compare(a: Routine, b: Routine): number
    {
        return a.name.localeCompare(b.name);
    }
}
