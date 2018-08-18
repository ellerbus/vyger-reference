using System.Collections.Generic;
using vyger.Core.Models;

namespace vyger.ViewModels
{
    public class ExerciseViewModel : Exercise
    {
        public ExerciseViewModel() { }

        public ExerciseViewModel(Exercise exercise)
        {
            Id = exercise.Id;
            Name = exercise.Name;
            GroupId = exercise.GroupId;
            CategoryId = exercise.CategoryId;
        }

        public IList<ExerciseGroup> Groups { get; set; }

        public IList<ExerciseCategory> Categories { get; set; }
    }
}