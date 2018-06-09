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
    public partial class WorkoutPlanExercise : Entities.WorkoutPlanExerciseEntity
    {
        #region Constructors

        public WorkoutPlanExercise(int planId, int cycleId, int exerciseId, int weight, int reps, int pullback, bool isCalculated, DateTime createdAt, DateTime? updatedAt)
            : base(planId, cycleId, exerciseId, weight, reps, pullback, isCalculated, createdAt, updatedAt)
        {
        }
        
        public WorkoutPlanExercise(WorkoutPlanExercise exercise)
			: this(exercise.PlanId, exercise.CycleId, exercise.ExerciseId, exercise.Weight, exercise.Reps, exercise.Pullback, exercise.IsCalculated, exercise.CreatedAt, exercise.UpdatedAt)
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
    [Table("WorkoutPlanExercise")]
    public abstract class WorkoutPlanExerciseEntity
    {
        #region Constructors

        protected WorkoutPlanExerciseEntity() { }

        protected WorkoutPlanExerciseEntity(int planId, int cycleId, int exerciseId, int weight, int reps, int pullback, bool isCalculated, DateTime createdAt, DateTime? updatedAt)
        {
            PlanId = planId;
            CycleId = cycleId;
            ExerciseId = exerciseId;
            Weight = weight;
            Reps = reps;
            Pullback = pullback;
            IsCalculated = isCalculated;
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
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Key]
        [Column("exercise_id", Order = 3)]
        public virtual int ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'weight'
        ///	</summary>
        [Column("weight", Order = 4)]
        public virtual int Weight { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'reps'
        ///	</summary>
        [Column("reps", Order = 5)]
        public virtual int Reps { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'pullback'
        ///	</summary>
        [Column("pullback", Order = 6)]
        public virtual int Pullback { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'is_calculated'
        ///	</summary>
        [Column("is_calculated", Order = 7)]
        public virtual bool IsCalculated { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 8)]
        public virtual DateTime CreatedAt { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 9)]
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}
