using System.Collections.Generic;
using vyger.Core.Models;

namespace vyger.ViewModels
{
    public class WorkoutLogCreateViewModel : WorkoutLog
    {
        public IList<Exercise> Exercises { get; set; }

        public IList<ExerciseGroup> Groups { get; set; }

        public IList<ExerciseCategory> Categories { get; set; }
    }
}