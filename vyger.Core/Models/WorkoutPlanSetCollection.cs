using System.Collections.Generic;
using System.Linq;

namespace vyger.Core.Models
{
    public class WorkoutPlanSetCollection : List<WorkoutPlanSet>
    {
        #region Constructors

        public WorkoutPlanSetCollection()
        {
        }

        public WorkoutPlanSetCollection(string workoutPlan)
        {
            IEnumerable<WorkoutPlanSet> sets = workoutPlan
                .Replace(" ", "")
                .Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => new WorkoutPlanSet(x));

            AddRange(sets);
        }

        #endregion

        #region Methods

        public static string Format(string workoutPlan)
        {
            WorkoutPlanSetCollection sets = new WorkoutPlanSetCollection(workoutPlan);

            return sets.Display;
        }

        public override string ToString()
        {
            return string.Join(", ", this.Select(x => x.Display));
        }

        #endregion

        #region Properties

        public string Display
        {
            get { return ToString(); }
        }

        #endregion
    }
}