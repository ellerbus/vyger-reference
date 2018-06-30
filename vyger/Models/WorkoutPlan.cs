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
    [XmlType("workout-plan")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutPlan
    {
        #region Constructors

        public WorkoutPlan()
        {
            Cycles = new WorkoutPlanCycleCollection(this);
        }

        public WorkoutPlan(WorkoutRoutine routine)
            : this()
        {
            Id = Constants.IdGenerator.Next();
            Routine = routine;
            CreatedAt = DateTime.UtcNow;
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
                string pk = $"[{Id}]";

                string uq = $"[{Routine?.Name}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("WorkoutPlan", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(WorkoutPlan other)
        {
            Status = other.Status;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'plan_id'
        ///	</summary>
        [DisplayName("Plan Id")]
        [XmlAttribute("plan-id")]
        public string Id { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'routine_id'
        ///	</summary>
        [Required]
        [DisplayName("Routine Id")]
        [XmlAttribute("routine-id")]
        public string RoutineId
        {
            get { return Routine == null ? _routineId : Routine.Id; }
            set { _routineId = value; }
        }

        private string _routineId;

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
        ///	Gets / Sets the foreign key to 'routine_id'
        ///	</summary>
        [XmlIgnore]
        public WorkoutRoutine Routine { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlArray("workout-plan-cycles"), XmlArrayItem("workout-plan-cycle")]
        public WorkoutPlanCycleCollection Cycles { get; private set; }

        #endregion
    }
}