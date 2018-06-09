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
    public partial class WorkoutLog : Entities.WorkoutLogEntity
    {
        #region Constructors

        public WorkoutLog(int memberId, DateTime logDate, int exerciseId, string workout, int planId, int cycleId, int weekId, int dayId, int sequenceNumber, DateTime createdAt, DateTime? updatedAt)
            : base(memberId, logDate, exerciseId, workout, planId, cycleId, weekId, dayId, sequenceNumber, createdAt, updatedAt)
        {
        }
        
        public WorkoutLog(WorkoutLog log)
			: this(log.MemberId, log.LogDate, log.ExerciseId, log.Workout, log.PlanId, log.CycleId, log.WeekId, log.DayId, log.SequenceNumber, log.CreatedAt, log.UpdatedAt)
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
    [Table("WorkoutLog")]
    public abstract class WorkoutLogEntity
    {
        #region Constructors

        protected WorkoutLogEntity() { }

        protected WorkoutLogEntity(int memberId, DateTime logDate, int exerciseId, string workout, int planId, int cycleId, int weekId, int dayId, int sequenceNumber, DateTime createdAt, DateTime? updatedAt)
        {
            MemberId = memberId;
            LogDate = logDate;
            ExerciseId = exerciseId;
            Workout = workout;
            PlanId = planId;
            CycleId = cycleId;
            WeekId = weekId;
            DayId = dayId;
            SequenceNumber = sequenceNumber;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        
        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'member_id'
        ///	</summary>
		[Key]
        [Column("member_id", Order = 1)]
        public virtual int MemberId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'log_date'
        ///	</summary>
		[Key]
        [Column("log_date", Order = 2)]
        public virtual DateTime LogDate { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Key]
        [Column("exercise_id", Order = 3)]
        public virtual int ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'workout'
        ///	</summary>
		[MaxLength(128)]
        [Column("workout", Order = 4)]
        public virtual string Workout { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'plan_id'
        ///	</summary>
        [Column("plan_id", Order = 5)]
        public virtual int PlanId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
        [Column("cycle_id", Order = 6)]
        public virtual int CycleId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'week_id'
        ///	</summary>
        [Column("week_id", Order = 7)]
        public virtual int WeekId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
        [Column("day_id", Order = 8)]
        public virtual int DayId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'sequence_number'
        ///	</summary>
        [Column("sequence_number", Order = 9)]
        public virtual int SequenceNumber { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 10)]
        public virtual DateTime CreatedAt { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 11)]
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}
