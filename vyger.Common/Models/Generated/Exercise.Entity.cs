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
            ExerciseId = exerciseId;
            OwnerId = ownerId;
            ExerciseName = exerciseName;
            GroupId = groupId;
            StatusEnum = statusEnum;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        
        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Key]
        [Column("exercise_id", Order = 1)]
        public virtual int ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'owner_id'
        ///	</summary>
        [Column("owner_id", Order = 2)]
        public virtual int OwnerId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_name'
        ///	</summary>
		[MaxLength(120)]
        [Column("exercise_name", Order = 3)]
        public virtual string ExerciseName { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'group_id'
        ///	</summary>
        [Column("group_id", Order = 4)]
        public virtual int GroupId { get; set; }

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
