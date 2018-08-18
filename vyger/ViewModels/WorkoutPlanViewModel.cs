using System.Collections.Generic;
using vyger.Core.Models;

namespace vyger.ViewModels
{
    public class WorkoutPlanViewModel : WorkoutPlan
    {
        public WorkoutPlanViewModel()
        {
        }

        public IList<WorkoutRoutine> Routines { get; set; }
    }
}