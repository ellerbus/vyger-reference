using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Augment;

namespace vyger.Common.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class WorkoutPlanExercise
    {
        #region Constructors

        public WorkoutPlanExercise() : base() { }

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
                string pk = $"[{PlanId}, {CycleId}, {ExerciseId}]";

                string uq = $"[{Cycle?.Plan?.Routine?.RoutineName}, {Exercise?.ExerciseName}]";

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
            IsCalculated = other.IsCalculated;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'plan_id'
        ///	</summary>
        [Required]
        [DisplayName("Plan Id")]
        public override int PlanId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
		[Required]
        [DisplayName("Cycle Id")]
        public override int CycleId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Required]
        [DisplayName("Exercise Id")]
        public override int ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'weight'
        ///	</summary>
		[Required]
        [DisplayName("Weight")]
        [Range(0, Constants.MaxWeight)]
        public override int Weight { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'reps'
        ///	</summary>
		[Required]
        [DisplayName("Reps")]
        [Range(0, Constants.MaxReps)]
        public override int Reps { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'pullback'
        ///	</summary>
		[Required]
        [DisplayName("Pullback")]
        [Range(0, 50)]
        public override int Pullback { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'is_calculated'
        ///	</summary>
		[Required]
        [DisplayName("Is Calculated")]
        public override bool IsCalculated { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public double OneRepMax
        {
            get { return WorkoutCalculator.OneRepMax(Weight, Reps); }
        }

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'cycle_id'
        ///	</summary>
        [ForeignKey(nameof(PlanId) + "," + nameof(CycleId))]
        public WorkoutPlanCycle Cycle { get; set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'exercise_id'
        ///	</summary>
        [ForeignKey(nameof(ExerciseId))]
        public Exercise Exercise { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<WorkoutPlanLog> PlanLogs { get; set; }

        #endregion
    }
}
