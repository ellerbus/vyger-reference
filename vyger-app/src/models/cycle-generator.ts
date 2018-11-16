import { Cycle } from './cycle';
import { CycleInput } from './cycle-input';
import { WorkoutSet, WorkoutSetTypes } from './workout-set';
import { utilities } from './utilities';
import { CycleCalculation } from './cycle-calculation';

export class CycleGenerator
{
    inputs: { [key: string]: CycleInput };

    calculations: CycleCalculation[];

    constructor(private cycle: Cycle)
    {
    }

    generate()
    {
        this.loadInputMap();
        this.loadCalculations();
        this.finalizeCalculations();
    }

    private loadInputMap = () =>
    {
        this.inputs = {};

        for (let i = 0; i < this.cycle.inputs.length; i++)
        {
            let ci = this.cycle.inputs[i];

            this.inputs[ci.id] = ci;
        }
    }

    private loadCalculations = () =>
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

    private replaceRepMaxes = (calculation: CycleCalculation) =>
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

    private replaceLookUps = (calculation: CycleCalculation) =>
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

    private finalizeCalculations = () =>
    {
        for (let i = 0; i < this.calculations.length; i++)
        {
            this.calculations[i].finalize();
        }
    }

    private lookupOneRepMax = (id: string): number =>
    {
        if (this.inputs[id])
        {
            return this.inputs[id].oneRepMax;
        }
        return 0;
    }
}
