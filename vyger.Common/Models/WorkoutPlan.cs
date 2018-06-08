using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Augment;

namespace vyger.Common.Models
{
    #region Extensions

    public static class WorkoutPlanExtensions
    {
        public static void CreateSetup(this WorkoutPlanCycle cycle, IEnumerable<WorkoutLog> logs)
        {
        }
    }

    #endregion

    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class WorkoutPlan
    {
        #region Constructors

        public WorkoutPlan() : base() { }

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
                string pk = $"[{PlanId}]";

                string uq = $"[{Routine?.RoutineName}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("WorkoutPlan", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(WorkoutPlan other)
        {
            Status = other.Status;
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
        ///	Gets / Sets database column 'owner_id'
        ///	</summary>
		[Required]
        [DisplayName("Owner Id")]
        public override int OwnerId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'routine_id'
        ///	</summary>
		[Required]
        [DisplayName("Routine Id")]
        public override int RoutineId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
		[Required]
        [DisplayName("Cycle Id")]
        [Range(Constants.MinCycles, Constants.MaxCycles)]
        public override int CycleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[NotMapped]
        public StatusTypes Status
        {
            get { return StatusEnum.ToEnum<StatusTypes>(); }
            set { StatusEnum = value.ToString(); }
        }

        public override string StatusEnum
        {
            get { return base.StatusEnum.AssertNotNull(StatusTypes.None.ToString()); }
            set { base.StatusEnum = value; }
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
        [ForeignKey(nameof(RoutineId))]
        public WorkoutRoutine Routine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<WorkoutPlanCycle> Cycles { get; set; }

        #endregion
    }
}
