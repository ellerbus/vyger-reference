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
            _routineId = routineId;
            _ownerId = ownerId;
            _routineName = routineName;
            _weeks = weeks;
            _days = days;
            _statusEnum = statusEnum;
            _createdAt = createdAt;
            _updatedAt = updatedAt;
        }
        
        #endregion

        #region Properties
        
        ///	<summary>
        ///	Internally Managed 'Is' Modified Flag
        ///	</summary>
        public bool IsModified { get; internal set; }

        ///	<summary>
        ///	Gets / Sets database column 'routine_id'
        ///	</summary>
		[Key]
        [Column("routine_id", Order = 1)]
        public virtual int RoutineId
        {
            get { return _routineId; }
            set { IsModified |= _routineId != value; _routineId = value; }
        }
        private int _routineId;

        ///	<summary>
        ///	Gets / Sets database column 'owner_id'
        ///	</summary>
        [Column("owner_id", Order = 2)]
        public virtual int OwnerId
        {
            get { return _ownerId; }
            set { IsModified |= _ownerId != value; _ownerId = value; }
        }
        private int _ownerId;

        ///	<summary>
        ///	Gets / Sets database column 'routine_name'
        ///	</summary>
		[MaxLength(120)]
        [Column("routine_name", Order = 3)]
        public virtual string RoutineName
        {
            get { return _routineName; }
            set { IsModified |= _routineName != value; _routineName = value; }
        }
        private string _routineName;

        ///	<summary>
        ///	Gets / Sets database column 'weeks'
        ///	</summary>
        [Column("weeks", Order = 4)]
        public virtual int Weeks
        {
            get { return _weeks; }
            set { IsModified |= _weeks != value; _weeks = value; }
        }
        private int _weeks;

        ///	<summary>
        ///	Gets / Sets database column 'days'
        ///	</summary>
        [Column("days", Order = 5)]
        public virtual int Days
        {
            get { return _days; }
            set { IsModified |= _days != value; _days = value; }
        }
        private int _days;

        ///	<summary>
        ///	Gets / Sets database column 'status_enum'
        ///	</summary>
		[MaxLength(15)]
        [Column("status_enum", Order = 6)]
        public virtual string StatusEnum
        {
            get { return _statusEnum; }
            set { IsModified |= _statusEnum != value; _statusEnum = value; }
        }
        private string _statusEnum;

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 7)]
        public virtual DateTime CreatedAt
        {
            get { return _createdAt; }
            set { IsModified |= _createdAt != value; _createdAt = value; }
        }
        private DateTime _createdAt;

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 8)]
        public virtual DateTime? UpdatedAt
        {
            get { return _updatedAt; }
            set { IsModified |= _updatedAt != value; _updatedAt = value; }
        }
        private DateTime? _updatedAt;

        #endregion
    }
}
