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
    public partial class WorkoutRoutine : Entities.WorkoutRoutineEntity
    {
        #region Constructors

        public WorkoutRoutine(int routineId, int ownerId, string routineName, int weeks, int days, string statusEnum, DateTime createdAt, DateTime? updatedAt)
            : base(routineId, ownerId, routineName, weeks, days, statusEnum, createdAt, updatedAt)
        {
        }
        
        public WorkoutRoutine(WorkoutRoutine routine)
			: this(routine.RoutineId, routine.OwnerId, routine.RoutineName, routine.Weeks, routine.Days, routine.StatusEnum, routine.CreatedAt, routine.UpdatedAt)
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
    [Table("WorkoutRoutine")]
    public abstract class WorkoutRoutineEntity
    {
        #region Constructors

        protected WorkoutRoutineEntity() { }

        protected WorkoutRoutineEntity(int routineId, int ownerId, string routineName, int weeks, int days, string statusEnum, DateTime createdAt, DateTime? updatedAt)
        {
            RoutineId = routineId;
            OwnerId = ownerId;
            RoutineName = routineName;
            Weeks = weeks;
            Days = days;
            StatusEnum = statusEnum;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        
        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'routine_id'
        ///	</summary>
		[Key]
        [Column("routine_id", Order = 1)]
        public virtual int RoutineId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'owner_id'
        ///	</summary>
        [Column("owner_id", Order = 2)]
        public virtual int OwnerId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'routine_name'
        ///	</summary>
		[MaxLength(120)]
        [Column("routine_name", Order = 3)]
        public virtual string RoutineName { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'weeks'
        ///	</summary>
        [Column("weeks", Order = 4)]
        public virtual int Weeks { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'days'
        ///	</summary>
        [Column("days", Order = 5)]
        public virtual int Days { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'status_enum'
        ///	</summary>
		[MaxLength(15)]
        [Column("status_enum", Order = 6)]
        public virtual string StatusEnum { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 7)]
        public virtual DateTime CreatedAt { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 8)]
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}
