using System.Collections.Generic;
using System.Diagnostics;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutPlanCollection : SingleKeyCollection<WorkoutPlan, string>
    {
        #region Constructors

        public WorkoutPlanCollection()
        {
        }

        public WorkoutPlanCollection(IEnumerable<WorkoutPlan> routines) : this()
        {
            AddRange(routines);
        }

        #endregion

        #region ToString/DebuggerDisplay

        ///	<summary>
        ///	DebuggerDisplay for this object
        ///	</summary>
        private string DebuggerDisplay
        {
            get { return "Count={0}".FormatArgs(Count); }
        }

        #endregion

        #region Methods

        protected override string GetPrimaryKey(WorkoutPlan item)
        {
            return item.Id;
        }

        public void AddRange(IEnumerable<WorkoutPlan> routines)
        {
            foreach (WorkoutPlan routine in routines)
            {
                Add(routine);
            }
        }

        #endregion
    }
}