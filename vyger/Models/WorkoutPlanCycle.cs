using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Serialization;
using Augment;
using vyger.Common;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [XmlType("workout-plan-cycle")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutPlanCycle
    {
        #region Constructors

        public WorkoutPlanCycle()
        {
            PlanExercises = new WorkoutPlanExerciseCollection(this);
            PlanLogs = new WorkoutPlanLogCollection(this);
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
        [XmlAttribute("cycle-id")]
        public int CycleId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlAttribute("status")]
        public StatusTypes Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlAttribute("created-at")]
        public DateTime CreatedAt { get; set; }

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'plan_id'
        ///	</summary>
        [XmlIgnore]
        public WorkoutPlan Plan { get; set; }

        ///	<summary>
        ///
        ///	</summary>
        [XmlArray("workout-plan-exercises"), XmlArrayItem("workout-plan-exercise")]
        public WorkoutPlanExerciseCollection PlanExercises { get; private set; }

        ///	<summary>
        ///
        ///	</summary>
        [XmlArray("workout-plan-logs"), XmlArrayItem("workout-plan-log")]
        public WorkoutPlanLogCollection PlanLogs { get; private set; }

        #endregion
    }
}