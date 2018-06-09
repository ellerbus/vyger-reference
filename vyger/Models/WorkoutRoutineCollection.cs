using System.Collections.Generic;
using System.Diagnostics;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutRoutineCollection : SingleKeyCollection<WorkoutRoutine, string>
    {
        #region Constructors

        public WorkoutRoutineCollection()
        {
        }

        public WorkoutRoutineCollection(IEnumerable<WorkoutRoutine> routines) : this()
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

        protected override string GetPrimaryKey(WorkoutRoutine item)
        {
            return item.Id;
        }

        public void AddRange(IEnumerable<WorkoutRoutine> routines)
        {
            foreach (WorkoutRoutine routine in routines)
            {
                Add(routine);
            }
        }

        #endregion

        #region Foreign Key Properties

        #endregion
    }
}
