using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Augment;

namespace vyger.Common.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class WorkoutPlanLog
    {
        #region Constructors

        public WorkoutPlanLog() : base() { }

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
                string pk = $"[{PlanId}, {CycleId}, {WeekId}, {DayId}, {ExerciseId}]";

                string uq = $"[{Exercise?.ExerciseName}]";

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
            StatusEnum = other.StatusEnum;
            CreatedAt = other.CreatedAt;
            UpdatedAt = other.UpdatedAt;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'plan_id'
        ///	</summary>
        [Required]
        [DisplayName("Plan Id")]
        public override int PlanId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
		[Required]
        [DisplayName("Cycle Id")]
        public override int CycleId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'week_id'
        ///	</summary>
		[Required]
        [DisplayName("Week Id")]
        public override int WeekId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
		[Required]
        [DisplayName("Day Id")]
        public override int DayId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Required]
        [DisplayName("Exercise Id")]
        public override int ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'sequence_number'
        ///	</summary>
		[Required]
        [DisplayName("Sequence Number")]
        public override int SequenceNumber { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'workout_plan'
        ///	</summary>
		[Required]
        [DisplayName("Workout Plan")]
        public override string WorkoutPlan { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[NotMapped]
        public StatusTypes Status
        {
            get { return StatusEnum.ToEnum<StatusTypes>(); }
            set { StatusEnum = value.ToString(); }
        }

        public override string StatusEnum
        {
            get { return base.StatusEnum.AssertNotNull(StatusTypes.None.ToString()); }
            set { base.StatusEnum = value; }
        }

        public override DateTime CreatedAt
        {
            get { return base.CreatedAt.EnsureUtc(); }
            set { base.CreatedAt = value.EnsureUtc(); }
        }

        public override DateTime? UpdatedAt
        {
            get { return base.UpdatedAt.EnsureUtc(); }
            set { base.UpdatedAt = value.EnsureUtc(); }
        }

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'cycle_id'
        ///	</summary>
        [ForeignKey(nameof(PlanId) + "," + nameof(CycleId) + "," + nameof(ExerciseId))]
        public WorkoutPlanExercise PlanExercise { get; set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'exercise_id'
        ///	</summary>
        [ForeignKey(nameof(ExerciseId))]
        public Exercise Exercise { get; set; }

        #endregion
    }
}
