using System.Collections.Generic;
using vyger.Common.Models;

namespace vyger.Web.Models
{
    public class WorkoutPlanForm : WorkoutPlan
    {
        public WorkoutPlanForm() { }

        public IList<WorkoutRoutine> Routines { get; set; }
    }
}