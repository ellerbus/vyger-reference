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
    public partial class WorkoutRoutineExercise : Entities.WorkoutRoutineExerciseEntity
    {
        #region Constructors

        public WorkoutRoutineExercise(int routineId, int weekId, int dayId, int exerciseId, int sequenceNumber, string workoutRoutine, DateTime createdAt, DateTime? updatedAt)
            : base(routineId, weekId, dayId, exerciseId, sequenceNumber, workoutRoutine, createdAt, updatedAt)
        {
        }
        
        public WorkoutRoutineExercise(WorkoutRoutineExercise exercise)
			: this(exercise.RoutineId, exercise.WeekId, exercise.DayId, exercise.ExerciseId, exercise.SequenceNumber, exercise.WorkoutRoutine, exercise.CreatedAt, exercise.UpdatedAt)
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
    [Table("WorkoutRoutineExercise")]
    public abstract class WorkoutRoutineExerciseEntity
    {
        #region Constructors

        protected WorkoutRoutineExerciseEntity() { }

        protected WorkoutRoutineExerciseEntity(int routineId, int weekId, int dayId, int exerciseId, int sequenceNumber, string workoutRoutine, DateTime createdAt, DateTime? updatedAt)
        {
            RoutineId = routineId;
            WeekId = weekId;
            DayId = dayId;
            ExerciseId = exerciseId;
            SequenceNumber = sequenceNumber;
            WorkoutRoutine = workoutRoutine;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        
        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'routine_id'
        ///	</summary>
		[Key]
        [Column("routine_id", Order = 1)]
        public virtual int RoutineId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'week_id'
        ///	</summary>
		[Key]
        [Column("week_id", Order = 2)]
        public virtual int WeekId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
		[Key]
        [Column("day_id", Order = 3)]
        public virtual int DayId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Key]
        [Column("exercise_id", Order = 4)]
        public virtual int ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'sequence_number'
        ///	</summary>
        [Column("sequence_number", Order = 5)]
        public virtual int SequenceNumber { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'workout_routine'
        ///	</summary>
		[MaxLength(128)]
        [Column("workout_routine", Order = 6)]
        public virtual string WorkoutRoutine { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 7)]
        public virtual DateTime CreatedAt { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 8)]
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}
