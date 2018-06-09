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
            _routineId = routineId;
            _weekId = weekId;
            _dayId = dayId;
            _exerciseId = exerciseId;
            _sequenceNumber = sequenceNumber;
            _workoutRoutine = workoutRoutine;
            _createdAt = createdAt;
            _updatedAt = updatedAt;
        }
        
        #endregion

        #region Properties
        
        ///	<summary>
        ///	Internally Managed 'Is' Modified Flag
        ///	</summary>
        public bool IsModified { get; internal set; }

        ///	<summary>
        ///	Gets / Sets database column 'routine_id'
        ///	</summary>
		[Key]
        [Column("routine_id", Order = 1)]
        public virtual int RoutineId
        {
            get { return _routineId; }
            set { IsModified |= _routineId != value; _routineId = value; }
        }
        private int _routineId;

        ///	<summary>
        ///	Gets / Sets database column 'week_id'
        ///	</summary>
		[Key]
        [Column("week_id", Order = 2)]
        public virtual int WeekId
        {
            get { return _weekId; }
            set { IsModified |= _weekId != value; _weekId = value; }
        }
        private int _weekId;

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
		[Key]
        [Column("day_id", Order = 3)]
        public virtual int DayId
        {
            get { return _dayId; }
            set { IsModified |= _dayId != value; _dayId = value; }
        }
        private int _dayId;

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Key]
        [Column("exercise_id", Order = 4)]
        public virtual int ExerciseId
        {
            get { return _exerciseId; }
            set { IsModified |= _exerciseId != value; _exerciseId = value; }
        }
        private int _exerciseId;

        ///	<summary>
        ///	Gets / Sets database column 'sequence_number'
        ///	</summary>
        [Column("sequence_number", Order = 5)]
        public virtual int SequenceNumber
        {
            get { return _sequenceNumber; }
            set { IsModified |= _sequenceNumber != value; _sequenceNumber = value; }
        }
        private int _sequenceNumber;

        ///	<summary>
        ///	Gets / Sets database column 'workout_routine'
        ///	</summary>
		[MaxLength(128)]
        [Column("workout_routine", Order = 6)]
        public virtual string WorkoutRoutine
        {
            get { return _workoutRoutine; }
            set { IsModified |= _workoutRoutine != value; _workoutRoutine = value; }
        }
        private string _workoutRoutine;

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 7)]
        public virtual DateTime CreatedAt
        {
            get { return _createdAt; }
            set { IsModified |= _createdAt != value; _createdAt = value; }
        }
        private DateTime _createdAt;

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 8)]
        public virtual DateTime? UpdatedAt
        {
            get { return _updatedAt; }
            set { IsModified |= _updatedAt != value; _updatedAt = value; }
        }
        private DateTime? _updatedAt;

        #endregion
    }
}
