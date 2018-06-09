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
    public partial class WorkoutPlan : Entities.WorkoutPlanEntity
    {
        #region Constructors

        public WorkoutPlan(int planId, int ownerId, int routineId, int cycleId, string statusEnum, DateTime createdAt, DateTime? updatedAt)
            : base(planId, ownerId, routineId, cycleId, statusEnum, createdAt, updatedAt)
        {
        }
        
        public WorkoutPlan(WorkoutPlan plan)
			: this(plan.PlanId, plan.OwnerId, plan.RoutineId, plan.CycleId, plan.StatusEnum, plan.CreatedAt, plan.UpdatedAt)
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
    [Table("WorkoutPlan")]
    public abstract class WorkoutPlanEntity
    {
        #region Constructors

        protected WorkoutPlanEntity() { }

        protected WorkoutPlanEntity(int planId, int ownerId, int routineId, int cycleId, string statusEnum, DateTime createdAt, DateTime? updatedAt)
        {
            PlanId = planId;
            OwnerId = ownerId;
            RoutineId = routineId;
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
        ///	Gets / Sets database column 'owner_id'
        ///	</summary>
        [Column("owner_id", Order = 2)]
        public virtual int OwnerId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'routine_id'
        ///	</summary>
        [Column("routine_id", Order = 3)]
        public virtual int RoutineId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
        [Column("cycle_id", Order = 4)]
        public virtual int CycleId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'status_enum'
        ///	</summary>
		[MaxLength(15)]
        [Column("status_enum", Order = 5)]
        public virtual string StatusEnum { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 6)]
        public virtual DateTime CreatedAt { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 7)]
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}
