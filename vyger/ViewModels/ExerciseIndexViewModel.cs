using System.Collections.Generic;
using vyger.Core.Models;

namespace vyger.ViewModels
{
    public class ExerciseIndexViewModel
    {
        public ExerciseIndexViewModel() { }

        public IList<Exercise> Items { get; set; }

        public string SelectedCategoryId { get; set; }

        public string SelectedGroupId { get; set; }
    }
}