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
    public class WorkoutPlanExercise
    {
        #region Constructors

        public WorkoutPlanExercise()
        {
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
                string pk = $"[{ExerciseId}]";

                string uq = $"[{Cycle?.Plan?.Routine?.Name}, {Exercise?.Name}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("WorkoutPlanExercise", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(WorkoutPlanExercise other)
        {
            Weight = other.Weight;
            Reps = other.Reps;
            Pullback = other.Pullback;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///
        ///	</summary>
        [Key]
        [Required]
        [DisplayName("Exercise Id")]
        public string ExerciseId
        {
            get { return Exercise == null ? _exerciseId : Exercise.Id; }
            set { _exerciseId = value; }
        }

        private string _exerciseId;

        ///	<summary>
        ///	Gets / Sets database column 'weight'
        ///	</summary>
        [Required]
        [DisplayName("Weight")]
        [Range(0, Constants.MaxWeight)]
        public int Weight { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'reps'
        ///	</summary>
        [Required]
        [DisplayName("Reps")]
        [Range(0, Constants.MaxReps)]
        public int Reps { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'pullback'
        ///	</summary>
        [Required]
        [DisplayName("Pullback")]
        [Range(0, 50)]
        public int Pullback { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'is_calculated'
        ///	</summary>
        [Required]
        [DisplayName("Is Calculated")]
        public bool IsCalculated { get; set; }

        /// <summary>
        ///
        /// </summary>
        [YamlIgnore]
        public double OneRepMax
        {
            get { return WorkoutCalculator.OneRepMax(Weight, Reps); }
        }

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'cycle_id'
        ///	</summary>
        public WorkoutPlanCycle Cycle { get; set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'exercise_id'
        ///	</summary>
        [YamlIgnore]
        public Exercise Exercise { get; set; }

        #endregion
    }
}