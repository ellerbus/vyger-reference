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
    public partial class ExerciseGroup : Entities.ExerciseGroupEntity
    {
        #region Constructors

        public ExerciseGroup(int groupId, string groupName, string statusEnum, DateTime createdAt, DateTime? updatedAt)
            : base(groupId, groupName, statusEnum, createdAt, updatedAt)
        {
        }
        
        public ExerciseGroup(ExerciseGroup group)
			: this(group.GroupId, group.GroupName, group.StatusEnum, group.CreatedAt, group.UpdatedAt)
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
    [Table("ExerciseGroup")]
    public abstract class ExerciseGroupEntity
    {
        #region Constructors

        protected ExerciseGroupEntity() { }

        protected ExerciseGroupEntity(int groupId, string groupName, string statusEnum, DateTime createdAt, DateTime? updatedAt)
        {
            GroupId = groupId;
            GroupName = groupName;
            StatusEnum = statusEnum;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        
        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'group_id'
        ///	</summary>
		[Key]
        [Column("group_id", Order = 1)]
        public virtual int GroupId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'group_name'
        ///	</summary>
		[MaxLength(120)]
        [Column("group_name", Order = 2)]
        public virtual string GroupName { get; set; }

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
