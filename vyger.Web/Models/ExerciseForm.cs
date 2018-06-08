using System.Collections.Generic;
using vyger.Common.Models;

namespace vyger.Web.Models
{
    public class ExerciseForm : Exercise
    {
        public ExerciseForm() { }

        public ExerciseForm(Exercise exercise) : base(exercise) { }

        public IList<ExerciseGroup> Groups { get; set; }
    }
}