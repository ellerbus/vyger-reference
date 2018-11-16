import { CycleExercise } from "./cycle-exercise";
import { WorkoutSet, WorkoutSetTypes } from "./workout-set";
import { utilities } from "./utilities";

export class CycleCalculation
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
