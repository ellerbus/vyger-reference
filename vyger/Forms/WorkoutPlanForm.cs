using System.Collections.Generic;
using vyger.Models;

namespace vyger.Forms
{
    public class WorkoutPlanForm : WorkoutPlan
    {
        public WorkoutPlanForm()
        {
        }

        public IList<WorkoutRoutine> Routines { get; set; }
    }
}