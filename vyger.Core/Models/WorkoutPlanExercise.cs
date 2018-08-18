using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Serialization;
using Augment;
using vyger.Core;

namespace vyger.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [XmlType("workout-plan-exercise")]
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
        [XmlAttribute("exercise-id")]
        public string ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'weight'
        ///	</summary>
        [Required]
        [DisplayName("Weight")]
        [Range(0, Constants.MaxWeight)]
        [XmlAttribute("weight")]
        public int Weight { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'reps'
        ///	</summary>
        [Required]
        [DisplayName("Reps")]
        [Range(0, Constants.MaxReps)]
        [XmlAttribute("reps")]
        public int Reps { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'pullback'
        ///	</summary>
        [Required]
        [DisplayName("Pullback")]
        [Range(0, 50)]
        [XmlAttribute("pullback")]
        public int Pullback { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'is_calculated'
        ///	</summary>
        [Required]
        [DisplayName("Is Calculated")]
        [XmlAttribute("is-calculated")]
        public bool IsCalculated { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore]
        public double OneRepMax
        {
            get { return WorkoutCalculator.OneRepMax(Weight, Reps); }
        }

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'cycle_id'
        ///	</summary>
        [XmlIgnore]
        public WorkoutPlanCycle Cycle { get; set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'exercise_id'
        ///	</summary>
        [XmlIgnore]
        public Exercise Exercise
        {
            get
            {
                if (Cycle == null || Cycle.Plan == null || Cycle.Plan.Routine == null)
                {
                    return null;
                }

                if (Cycle.Plan.Routine.AllExercises == null)
                {
                    return null;
                }

                Exercise ex = null;

                Cycle.Plan.Routine.AllExercises.TryGetByPrimaryKey(ExerciseId, out ex);

                return ex;
            }
        }

        #endregion
    }
}