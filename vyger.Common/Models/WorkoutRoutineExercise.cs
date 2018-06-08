using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using Augment;

namespace vyger.Common.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class WorkoutRoutineExercise
    {
        #region Constructors

        public WorkoutRoutineExercise() : base() { }

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
        public override int RoutineId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'week_id'
        ///	</summary>
        [Required]
        [Range(1, 9)]
        [DisplayName("Week Id")]
        public override int WeekId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
        [Required]
        [Range(1, 7)]
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
        ///	Gets / Sets database column 'workout_routine'
        ///	</summary>
        [Required]
        [DisplayName("Workout Routine")]
        public override string WorkoutRoutine { get; set; }

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
        [ForeignKey(nameof(RoutineId))]
        public WorkoutRoutine Routine { get; set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'exercise_id'
        ///	</summary>
        [ForeignKey(nameof(ExerciseId))]
        public Exercise Exercise { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NotMapped]
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
