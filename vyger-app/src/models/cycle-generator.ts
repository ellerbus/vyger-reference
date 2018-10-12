import { Cycle } from './cycle';
import { CycleInput } from './cycle-input';
import { WorkoutSet, WorkoutSetTypes } from './workout-set';
import { utilities } from './utilities';
import { CycleExercise } from './cycle-exercise';

class CycleCalculation
{
    exercise: CycleExercise;
    sets: WorkoutSet[];

    finalize()
    {
        for (let j = 0; j < this.sets.length; j++)
        {
            let set = this.sets[j];

            if (set.type == WorkoutSetTypes.Static)
            {
                set.weight = utilities.round(set.weight, 5);
            }
        }

        this.exercise.plan = this.sets.map(x => x.pattern);
    }
}

export class CycleGenerator
{
    inputs: { [key: string]: CycleInput };

    calculations: CycleCalculation[];

    constructor(private cycle: Cycle)
    {
    }

    generate()
    {
        this.loadInputs();
        this.loadCalculations();
        this.finalizeCalculations();
    }

    loadInputs()
    {
        this.inputs = {};

        for (let i = 0; i < this.cycle.inputs.length; i++)
        {
            let ci = this.cycle.inputs[i];

            this.inputs[ci.id] = ci;
        }
    }

    loadCalculations()
    {
        this.calculations = [];

        for (let i = 0; i < this.cycle.exercises.length; i++)
        {
            let e = this.cycle.exercises[i];

            let c = new CycleCalculation();

            c.exercise = e;

            c.sets = c.exercise.sets.map(x => new WorkoutSet(x));

            this.replaceRepMaxes(c);

            this.replaceLookUps(c);

            this.calculations.push(c);
        }
    }

    replaceRepMaxes(calculation: CycleCalculation)
    {
        for (let i = 0; i < calculation.sets.length; i++)
        {
            let set = calculation.sets[i];

            if (set.type == WorkoutSetTypes.RepMax)
            {
                let orm = this.lookupOneRepMax(calculation.exercise.id);

                set.weight = utilities.prediction(orm, set.repmax) * (set.percent / 100.0);

                set.type = WorkoutSetTypes.Static;
            }
        }
    }

    replaceLookUps(calculation: CycleCalculation)
    {
        for (let i = 0; i < calculation.sets.length; i++)
        {
            let set = calculation.sets[i];

            if (set.type == WorkoutSetTypes.Reference)
            {
                if (set.reference == 'L')
                {
                    let idx = calculation.sets.length - 1;

                    set.weight = calculation.sets[idx].weight * (set.percent / 100.0);

                    set.type = WorkoutSetTypes.Static;
                }
            }
        }
    }

    finalizeCalculations()
    {
        for (let i = 0; i < this.calculations.length; i++)
        {
            this.calculations[i].finalize();
        }
    }

    lookupOneRepMax(id: string): number
    {
        if (this.inputs[id])
        {
            return this.inputs[id].oneRepMax;
        }
        return 0;
    }
}
