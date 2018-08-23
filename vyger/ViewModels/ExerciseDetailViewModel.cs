using System.Collections.Generic;
using vyger.Core.Models;

namespace vyger.ViewModels
{
    public class ExerciseDetailViewModel : Exercise
    {
        public ExerciseDetailViewModel() { }

        public ExerciseDetailViewModel(Exercise exercise)
        {
            Id = exercise.Id;
            Name = exercise.Name;
            Group = exercise.Group;
            Category = exercise.Category;
            GroupId = exercise.GroupId;
            CategoryId = exercise.CategoryId;
        }

        public IList<ExerciseGroup> Groups { get; set; }

        public IList<ExerciseCategory> Categories { get; set; }
    }
}