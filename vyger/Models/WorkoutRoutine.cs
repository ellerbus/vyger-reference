using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Augment;
using vyger.Common;
using YamlDotNet.Serialization;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutRoutine
    {
        #region Constructors

        public WorkoutRoutine()
        {
            RoutineExercises = new WorkoutRoutineExerciseCollection(this, new WorkoutRoutineExercise[0]);
        }

        public WorkoutRoutine(WorkoutRoutine routine)
            : this()
        {
            Id = routine.Id;
            Name = routine.Name;
            Weeks = routine.Weeks;
            Days = routine.Days;
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
                string id = $"[{Id}]";

                string nm = $"[{Name}]";

                return "{0}, id={1}, nm={2}".FormatArgs(nameof(WorkoutRoutine), id, nm);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(WorkoutRoutine other)
        {
            Name = other.Name;
            Weeks = other.Weeks;
            Days = other.Days;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///
        ///	</summary>
        [Required]
        [DisplayName("ID")]
        [MinLength(1), MaxLength(1)]
        public string Id
        {
            get { return _id; }
            set { _id = value.ToUpper(); }
        }

        private string _id;

        ///	<summary>
        ///
        ///	</summary>
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        ///	<summary>
        ///
        ///	</summary>
        [Required]
        [DisplayName("Weeks")]
        [Range(Constants.MinWeeks, Constants.MaxWeeks)]
        public int Weeks { get; set; }

        ///	<summary>
        ///
        ///	</summary>
        [Required]
        [DisplayName("Days")]
        [Range(Constants.MinDays, Constants.MaxDays)]
        public int Days { get; set; }

        #endregion

        #region Foreign Key Properties

        /// <summary>
        ///
        /// </summary>
        [YamlIgnore]
        public ExerciseCollection AllExercises { get; set; }

        /// <summary>
        ///
        /// </summary>
        public WorkoutRoutineExerciseCollection RoutineExercises
        {
            get { return _routineExercises; }
            set
            {
                _routineExercises = new WorkoutRoutineExerciseCollection(this, value);
            }
        }

        private WorkoutRoutineExerciseCollection _routineExercises;

        #endregion
    }
}