using System.Collections.Generic;
using vyger.Common.Models;

namespace vyger.Web.Models
{
    public class WorkoutRoutineExerciseForm
    {
        public WorkoutRoutineExerciseForm() { }

        public WorkoutRoutine Routine { get; set; }

        public IList<Exercise> Exercises { get; set; }

        public Exercise Exercise { get; set; }

        public WorkoutRoutineExercise RoutineExercise { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }
    }
}