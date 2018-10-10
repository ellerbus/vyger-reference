import { utilities } from 'src/models/utilities';
import { CycleExercise } from './cycle-exercise';
import { CycleInput } from './cycle-input';

//  inputing vs planned

export class Cycle
{
    id: string = utilities.generateId('c', 3);
    name: string;
    sequence: number;
    weeks: number;
    days: number;
    inputs: CycleInput[] = [];
    exercises: CycleExercise[] = [];

    constructor(source?: any)
    {
        const keys = ['id', 'name', 'sequence', 'weeks', 'days'];

        utilities.extend(this, source, keys);

        if (source && source.exercises)
        {
            this.exercises = source.exercises.map(x => new CycleExercise(x));
        }

        if (source && source.inputs)
        {
            this.inputs = source.inputs.map(x => new CycleInput(x));
        }
    }

    static compare(a: Cycle, b: Cycle): number
    {
        //  desc
        const seq = b.sequence - a.sequence;

        if (seq != 0)
        {
            return seq;
        }

        return a.name.localeCompare(b.name);
    }
}
