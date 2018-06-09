using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Augment;
using vyger.Common.Collections;

namespace vyger.Common.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class WorkoutRoutineExercise
    {
        #region Constructors

        public WorkoutRoutineExercise() { }

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
                string pk = $"[{RoutineId}, {WeekId}, {DayId}, {ExerciseId}]";

                string uq = $"[{Exercise?.ExerciseName}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("WorkoutRoutineExercise", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(WorkoutRoutineExercise other)
        {
            SequenceNumber = other.SequenceNumber;
            WorkoutRoutine = other.WorkoutRoutine;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'routine_id'
        ///	</summary>
        [Required]
        [DisplayName("Routine Id")]
        public override int RoutineId
        {
            get { return Routine == null ? base.RoutineId : Routine.RoutineId; }
            set { base.RoutineId = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'week_id'
        ///	</summary>
		[Required]
        [DisplayName("Week Id")]
        public override int WeekId
        {
            get { return base.WeekId; }
            set { base.WeekId = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
		[Required]
        [DisplayName("Day Id")]
        public override int DayId
        {
            get { return base.DayId; }
            set { base.DayId = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Required]
        [DisplayName("Exercise Id")]
        public override int ExerciseId
        {
            get { return Exercise == null ? base.ExerciseId : Exercise.ExerciseId; }
            set { base.ExerciseId = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'sequence_number'
        ///	</summary>
		[Required]
        [DisplayName("Sequence Number")]
        public override int SequenceNumber
        {
            get { return base.SequenceNumber; }
            set { base.SequenceNumber = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'workout_routine'
        ///	</summary>
		[Required]
        [DisplayName("Workout Routine")]
        public override string WorkoutRoutine
        {
            get { return base.WorkoutRoutine; }
            set
            {
                base.WorkoutRoutine = value;

                _sets = null;
            }
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
        ///	Gets / Sets the foreign key to 'routine_id'
        ///	</summary>
        public WorkoutRoutine Routine { get; set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'exercise_id'
        ///	</summary>
        public Exercise Exercise { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WorkoutRoutineSetCollection Sets
        {
            get
            {
                if (_sets == null)
                {
                    _sets = new WorkoutRoutineSetCollection(WorkoutRoutine);
                }
                return _sets;
            }
        }
        private WorkoutRoutineSetCollection _sets;
        #endregion
    }
}
