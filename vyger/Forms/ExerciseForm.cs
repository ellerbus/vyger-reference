using System.Collections.Generic;
using vyger.Models;

namespace vyger.Forms
{
    public class ExerciseForm : Exercise
    {
        public ExerciseForm() { }

        public ExerciseForm(Exercise exercise)
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