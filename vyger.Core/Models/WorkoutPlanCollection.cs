using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using Augment;

namespace vyger.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [XmlRoot("workout-plans")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutPlanCollection : SingleKeyCollection<WorkoutPlan, string>
    {
        #region Constructors

        public WorkoutPlanCollection()
        {
        }

        public WorkoutPlanCollection(WorkoutRoutineCollection routines, IEnumerable<WorkoutPlan> plans)
            : this()
        {
            Routines = routines;

            AddRange(plans);
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

        protected override void InsertItem(int index, WorkoutPlan item)
        {
            base.InsertItem(index, item);

            UpdateReferences(item);
        }

        protected override void SetItem(int index, WorkoutPlan item)
        {
            base.SetItem(index, item);

            UpdateReferences(item);
        }

        private void UpdateReferences(WorkoutPlan item)
        {
            if (Routines != null)
            {
                item.Routine = Routines.GetByPrimaryKey(item.RoutineId);
            }
        }

        public void AddRange(IEnumerable<WorkoutPlan> plans)
        {
            if (plans != null)
            {
                foreach (WorkoutPlan plan in plans)
                {
                    Add(plan);
                }
            }
        }

        #endregion

        #region Foreign Keys

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore]
        public WorkoutRoutineCollection Routines { get; private set; }

        #endregion
    }
}