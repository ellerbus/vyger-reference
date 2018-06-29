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
    [XmlRoot("workout-plan-log")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutPlanLog
    {
        #region Constructors

        public WorkoutPlanLog() : base()
        {
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
                string pk = $"[{WeekId}, {DayId}, {ExerciseId}]";

                string uq = $"[{PlanExercise?.Exercise?.Name}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("WorkoutPlanLog", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(WorkoutPlanLog other)
        {
            SequenceNumber = other.SequenceNumber;
            WorkoutPlan = other.WorkoutPlan;
            Status = other.Status;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'week_id'
        ///	</summary>
        [Required]
        [DisplayName("Week Id")]
        [XmlAttribute("week-id")]
        public int WeekId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
        [Required]
        [DisplayName("Day Id")]
        [XmlAttribute("day-id")]
        public int DayId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
        [Required]
        [DisplayName("Exercise Id")]
        [XmlAttribute("exercise-id")]
        public string ExerciseId
        {
            get { return PlanExercise == null ? _exerciseId : PlanExercise.Exercise.Id; }
            set { _exerciseId = value; }
        }

        private string _exerciseId;

        ///	<summary>
        ///	Gets / Sets database column 'sequence_number'
        ///	</summary>
        [Required]
        [DisplayName("Sequence Number")]
        [XmlAttribute("sequence-number")]
        public int SequenceNumber { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'workout_plan'
        ///	</summary>
        [Required]
        [DisplayName("Workout Plan")]
        [XmlAttribute("workout-plan")]
        public string WorkoutPlan { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlAttribute("status")]
        public StatusTypes Status { get; set; }

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'cycle_id'
        ///	</summary>
        [XmlIgnore]
        public WorkoutPlanCycle Cycle { get; set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'cycle_id'
        ///	</summary>
        [XmlIgnore]
        public WorkoutPlanExercise PlanExercise { get; set; }

        #endregion
    }
}