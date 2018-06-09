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
    public partial class Exercise : Entities.ExerciseEntity
    {
        #region Constructors

        public Exercise(int exerciseId, int ownerId, string exerciseName, int groupId, string statusEnum, DateTime createdAt, DateTime? updatedAt)
            : base(exerciseId, ownerId, exerciseName, groupId, statusEnum, createdAt, updatedAt)
        {
        }
        
        public Exercise(Exercise exercise)
			: this(exercise.ExerciseId, exercise.OwnerId, exercise.ExerciseName, exercise.GroupId, exercise.StatusEnum, exercise.CreatedAt, exercise.UpdatedAt)
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
    [Table("Exercise")]
    public abstract class ExerciseEntity
    {
        #region Constructors

        protected ExerciseEntity() { }

        protected ExerciseEntity(int exerciseId, int ownerId, string exerciseName, int groupId, string statusEnum, DateTime createdAt, DateTime? updatedAt)
        {
            _exerciseId = exerciseId;
            _ownerId = ownerId;
            _exerciseName = exerciseName;
            _groupId = groupId;
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
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Key]
        [Column("exercise_id", Order = 1)]
        public virtual int ExerciseId
        {
            get { return _exerciseId; }
            set { IsModified |= _exerciseId != value; _exerciseId = value; }
        }
        private int _exerciseId;

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
        ///	Gets / Sets database column 'exercise_name'
        ///	</summary>
		[MaxLength(120)]
        [Column("exercise_name", Order = 3)]
        public virtual string ExerciseName
        {
            get { return _exerciseName; }
            set { IsModified |= _exerciseName != value; _exerciseName = value; }
        }
        private string _exerciseName;

        ///	<summary>
        ///	Gets / Sets database column 'group_id'
        ///	</summary>
        [Column("group_id", Order = 4)]
        public virtual int GroupId
        {
            get { return _groupId; }
            set { IsModified |= _groupId != value; _groupId = value; }
        }
        private int _groupId;

        ///	<summary>
        ///	Gets / Sets database column 'status_enum'
        ///	</summary>
		[MaxLength(15)]
        [Column("status_enum", Order = 5)]
        public virtual string StatusEnum
        {
            get { return _statusEnum; }
            set { IsModified |= _statusEnum != value; _statusEnum = value; }
        }
        private string _statusEnum;

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 6)]
        public virtual DateTime CreatedAt
        {
            get { return _createdAt; }
            set { IsModified |= _createdAt != value; _createdAt = value; }
        }
        private DateTime _createdAt;

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 7)]
        public virtual DateTime? UpdatedAt
        {
            get { return _updatedAt; }
            set { IsModified |= _updatedAt != value; _updatedAt = value; }
        }
        private DateTime? _updatedAt;

        #endregion
    }
}
