using System;
using System.Collections.Generic;
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
    public partial class WorkoutPlanCycle
    {
        #region Constructors

        public WorkoutPlanCycle() : base() { }

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
                string pk = $"[{PlanId}, {CycleId}]";

                string uq = $"[{Plan?.Routine?.RoutineName}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("WorkoutPlanCycle", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(WorkoutPlanCycle other)
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
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
		[Required]
        [DisplayName("Cycle Id")]
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
        ///	Gets / Sets the foreign key to 'plan_id'
        ///	</summary>
        [ForeignKey(nameof(PlanId))]
        public WorkoutPlan Plan { get; set; }

        ///	<summary>
        ///	
        ///	</summary>
        public IList<WorkoutPlanExercise> PlanExercises { get; set; } = new List<WorkoutPlanExercise>();

        #endregion
    }
}
