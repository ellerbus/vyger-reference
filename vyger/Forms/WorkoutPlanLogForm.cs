using System.Collections.Generic;
using System.Linq;
using vyger.Common.Models;

namespace vyger.Web.Models
{
    public class WorkoutPlanLogForm
    {
        public WorkoutPlanLogForm() { }

        public WorkoutPlanCycle Cycle { get; set; }

        public WorkoutRoutine Routine { get; set; }

        public IEnumerable<WorkoutPlanLog> GetLogsFor(int weekId, int dayId)
        {
            return Cycle
                .PlanExercises
                .SelectMany(x => x.PlanLogs)
                .Where(x => x.WeekId == weekId && x.DayId == dayId)
                .OrderBy(x => x.SequenceNumber);
        }
    }
}