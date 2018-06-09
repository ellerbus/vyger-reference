using System.Collections.Generic;
using System.Diagnostics;
using Augment;
using vyger.Common.Models;

namespace vyger.Common.Collections
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutRoutineCollection : SingleKeyCollection<WorkoutRoutine, int>
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

        protected override int GetPrimaryKey(WorkoutRoutine item)
        {
            return item.RoutineId;
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
