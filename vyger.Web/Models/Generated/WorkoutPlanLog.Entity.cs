using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace vyger.Common.Models
{
    ///	<summary>
    ///
    ///	</summary>
    public partial class WorkoutPlanLog : Entities.WorkoutPlanLogEntity
    {
        #region Constructors

        public WorkoutPlanLog(int planId, int cycleId, int weekId, int dayId, int exerciseId, int sequenceNumber, string workoutPlan, string statusEnum, DateTime createdAt, DateTime? updatedAt)
            : base(planId, cycleId, weekId, dayId, exerciseId, sequenceNumber, workoutPlan, statusEnum, createdAt, updatedAt)
        {
        }

        public WorkoutPlanLog(WorkoutPlanLog log)
            : this(log.PlanId, log.CycleId, log.WeekId, log.DayId, log.ExerciseId, log.SequenceNumber, log.WorkoutPlan, log.StatusEnum, log.CreatedAt, log.UpdatedAt)
        {
        }

        #endregion
    }
}

namespace vyger.Common.Models.Entities
{
    ///	<summary>
    ///
    ///	</summary>
    [Table("WorkoutPlanLog")]
    public abstract class WorkoutPlanLogEntity
    {
        #region Constructors

        protected WorkoutPlanLogEntity() { }

        protected WorkoutPlanLogEntity(int planId, int cycleId, int weekId, int dayId, int exerciseId, int sequenceNumber, string workoutPlan, string statusEnum, DateTime createdAt, DateTime? updatedAt)
        {
            PlanId = planId;
            CycleId = cycleId;
            WeekId = weekId;
            DayId = dayId;
            ExerciseId = exerciseId;
            SequenceNumber = sequenceNumber;
            WorkoutPlan = workoutPlan;
            StatusEnum = statusEnum;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'plan_id'
        ///	</summary>
        [Key]
        [Column("plan_id", Order = 1)]
        public virtual int PlanId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
		[Key]
        [Column("cycle_id", Order = 2)]
        public virtual int CycleId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'week_id'
        ///	</summary>
		[Key]
        [Column("week_id", Order = 3)]
        public virtual int WeekId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
		[Key]
        [Column("day_id", Order = 4)]
        public virtual int DayId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Key]
        [Column("exercise_id", Order = 5)]
        public virtual int ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'sequence_number'
        ///	</summary>
        [Column("sequence_number", Order = 6)]
        public virtual int SequenceNumber { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'workout_plan'
        ///	</summary>
		[MaxLength(128)]
        [Column("workout_plan", Order = 7)]
        public virtual string WorkoutPlan { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'status_enum'
        ///	</summary>
		[MaxLength(15)]
        [Column("status_enum", Order = 8)]
        public virtual string StatusEnum { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 9)]
        public virtual DateTime CreatedAt { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 10)]
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}
