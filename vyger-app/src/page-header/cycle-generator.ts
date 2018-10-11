import { Cycle } from "src/models/cycle";
import { CycleInput } from "src/models/cycle-input";
import { WorkoutSet, WorkoutSetTypes } from "src/models/workout-set";
import { utilities } from "src/models/utilities";
import { CycleExercise } from "src/models/cycle-exercise";

class CycleCalculation
{
    exercise: CycleExercise;
    sets: WorkoutSet[];
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
        this.replaceRepMaxes();
        this.replaceLookUps();
        this.finalizeCalculations()
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

            this.calculations.push(c);
        }
    }

    replaceRepMaxes()
    {
        for (let i = 0; i < this.calculations.length; i++)
        {
            let calculation = this.calculations[i];

            for (let j = 0; j < calculation.sets.length; j++)
            {
                let set = calculation.sets[j];

                if (set.type == WorkoutSetTypes.RepMax)
                {
                    let orm = this.getOneRepMax(calculation.exercise.id);

                    set.weight = utilities.prediction(orm, set.repmax) * (set.percent / 100.0);

                    set.type = WorkoutSetTypes.Static;
                }
            }
        }
    }

    replaceLookUps()
    {
        for (let i = 0; i < this.calculations.length; i++)
        {
            let calculation = this.calculations[i];

            for (let j = 0; j < calculation.sets.length; j++)
            {
                let set = calculation.sets[j];

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
    }

    finalizeCalculations()
    {
        for (let i = 0; i < this.calculations.length; i++)
        {
            let calculation = this.calculations[i];

            for (let j = 0; j < calculation.sets.length; j++)
            {
                let set = calculation.sets[j];

                if (set.type == WorkoutSetTypes.Static)
                {
                    set.weight = utilities.round(set.weight, 5);
                }
            }

            calculation.exercise.plan = calculation.sets.map(x => x.pattern);
        }
    }

    getOneRepMax(id: string): number
    {
        if (this.inputs[id])
        {
            return this.inputs[id].oneRepMax;
        }
        return 0;
    }
}
