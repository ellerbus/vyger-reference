using System.Collections.Generic;
using vyger.Core.Models;

namespace vyger.ViewModels
{
    public class WorkoutRoutineExerciseCreateViewModel
    {
        public WorkoutRoutine Routine { get; set; }

        public WorkoutRoutineExercise RoutineExercise { get; set; }

        public IList<Exercise> Exercises { get; set; }

        public IList<ExerciseGroup> Groups { get; set; }

        public IList<ExerciseCategory> Categories { get; set; }
    }
}