using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Augment;
using vyger.Common;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutPlanCycle
    {
        #region Constructors

        public WorkoutPlanCycle()
        {
            PlanExercises = new WorkoutPlanExerciseCollection(this, new WorkoutPlanExercise[0]);
            PlanLogs = new WorkoutPlanLogCollection(this, new WorkoutPlanLog[0]);
        }

        #endregion

        #region ToString/DebuggerDisplay

        public override string ToString()
        {
            return DebuggerDisplay;
        }

        ///	<summary>
        ///	DebuggerDisplay for this object
        ///	</summary>
        private string DebuggerDisplay
        {
            get
            {
                string pk = $"[ {CycleId}]";

                string uq = $"[{Plan?.Routine?.Name}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("WorkoutPlanCycle", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(WorkoutPlanCycle other)
        {
            Status = other.Status;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
        [Required]
        [DisplayName("Cycle Id")]
        public int CycleId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public StatusTypes Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime CreatedAt { get; set; }

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'plan_id'
        ///	</summary>
        public WorkoutPlan Plan { get; set; }

        ///	<summary>
        ///
        ///	</summary>
        public WorkoutPlanExerciseCollection PlanExercises { get; set; }

        ///	<summary>
        ///
        ///	</summary>
        public WorkoutPlanLogCollection PlanLogs { get; set; }

        #endregion
    }
}