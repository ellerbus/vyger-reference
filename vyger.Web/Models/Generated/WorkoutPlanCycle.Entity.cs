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
    public partial class WorkoutPlanCycle : Entities.WorkoutPlanCycleEntity
    {
        #region Constructors

        public WorkoutPlanCycle(int planId, int cycleId, string statusEnum, DateTime createdAt, DateTime? updatedAt)
            : base(planId, cycleId, statusEnum, createdAt, updatedAt)
        {
        }
        
        public WorkoutPlanCycle(WorkoutPlanCycle cycle)
			: this(cycle.PlanId, cycle.CycleId, cycle.StatusEnum, cycle.CreatedAt, cycle.UpdatedAt)
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
    [Table("WorkoutPlanCycle")]
    public abstract class WorkoutPlanCycleEntity
    {
        #region Constructors

        protected WorkoutPlanCycleEntity() { }

        protected WorkoutPlanCycleEntity(int planId, int cycleId, string statusEnum, DateTime createdAt, DateTime? updatedAt)
        {
            PlanId = planId;
            CycleId = cycleId;
            StatusEnum = statusEnum;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        
        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'plan_id'
        ///	</summary>
		[Key]
        [Column("plan_id", Order = 1)]
        public virtual int PlanId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
		[Key]
        [Column("cycle_id", Order = 2)]
        public virtual int CycleId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'status_enum'
        ///	</summary>
		[MaxLength(15)]
        [Column("status_enum", Order = 3)]
        public virtual string StatusEnum { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 4)]
        public virtual DateTime CreatedAt { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 5)]
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}
