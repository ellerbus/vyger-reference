using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Augment;
using YamlDotNet.Serialization;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutRoutineExercise
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
                string id = $"[{WeekId}, {DayId}, {ExerciseId}]";

                string nm = $"[{Exercise?.Name}]";

                return "{0}, id={1}, nm={2}".FormatArgs("WorkoutRoutineExercise", id, nm);
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
        ///	
        ///	</summary>
		[Required]
        [DisplayName("Week Id")]
        public int WeekId { get; set; }

        ///	<summary>
        ///	
        ///	</summary>
		[Required]
        [DisplayName("Day Id")]
        public int DayId { get; set; }

        ///	<summary>
        ///	
        ///	</summary>
		[Required]
        [DisplayName("Exercise Id")]
        public string ExerciseId
        {
            get { return Exercise == null ? _exerciseId : Exercise.Id; }
            set { _exerciseId = value; }
        }
        private string _exerciseId;

        ///	<summary>
        ///	
        ///	</summary>
		[Required]
        [DisplayName("Sequence Number")]
        public int SequenceNumber { get; set; }

        ///	<summary>
        ///	
        ///	</summary>
		[Required]
        [DisplayName("Workout Routine")]
        public string WorkoutRoutine
        {
            get { return _workoutRoutine; }
            set
            {
                _workoutRoutine = value;

                _sets = null;
            }
        }
        private string _workoutRoutine;

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'routine_id'
        ///	</summary>
        [YamlIgnore]
        public WorkoutRoutine Routine { get; set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'exercise_id'
        ///	</summary>
        [YamlIgnore]
        public Exercise Exercise { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [YamlIgnore]
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
