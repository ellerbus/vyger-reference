using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vyger.Common.Models
{
    public class WorkoutPlanSetCollection : List<WorkoutPlanSet>
    {
        #region Constructors

        public WorkoutPlanSetCollection()
        {
        }

        public WorkoutPlanSetCollection(string workoutPlan)
        {
            IEnumerable<WorkoutPlanSet> sets = workoutPlan.ToUpper()
                .Replace(" ", "")
                .Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => new WorkoutPlanSet(x));

            AddRange(sets);
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (WorkoutPlanSet set in this)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }

                sb.Append(set.Display);
            }

            return sb.ToString();
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
