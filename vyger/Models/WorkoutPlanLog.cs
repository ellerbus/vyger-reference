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
        public int WeekId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
        [Required]
        [DisplayName("Day Id")]
        public int DayId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
        [Required]
        [DisplayName("Exercise Id")]
        public string ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'sequence_number'
        ///	</summary>
        [Required]
        [DisplayName("Sequence Number")]
        public int SequenceNumber { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'workout_plan'
        ///	</summary>
        [Required]
        [DisplayName("Workout Plan")]
        public string WorkoutPlan { get; set; }

        /// <summary>
        ///
        /// </summary>
        public StatusTypes Status { get; set; }

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'cycle_id'
        ///	</summary>
        public WorkoutPlanCycle Cycle { get; set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'cycle_id'
        ///	</summary>
        public WorkoutPlanExercise PlanExercise { get; set; }

        #endregion
    }
}