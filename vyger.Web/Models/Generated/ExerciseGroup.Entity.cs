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
            _groupId = groupId;
            _groupName = groupName;
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
        ///	Gets / Sets database column 'group_id'
        ///	</summary>
		[Key]
        [Column("group_id", Order = 1)]
        public virtual int GroupId
        {
            get { return _groupId; }
            set { IsModified |= _groupId != value; _groupId = value; }
        }
        private int _groupId;

        ///	<summary>
        ///	Gets / Sets database column 'group_name'
        ///	</summary>
		[MaxLength(120)]
        [Column("group_name", Order = 2)]
        public virtual string GroupName
        {
            get { return _groupName; }
            set { IsModified |= _groupName != value; _groupName = value; }
        }
        private string _groupName;

        ///	<summary>
        ///	Gets / Sets database column 'status_enum'
        ///	</summary>
		[MaxLength(15)]
        [Column("status_enum", Order = 3)]
        public virtual string StatusEnum
        {
            get { return _statusEnum; }
            set { IsModified |= _statusEnum != value; _statusEnum = value; }
        }
        private string _statusEnum;

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 4)]
        public virtual DateTime CreatedAt
        {
            get { return _createdAt; }
            set { IsModified |= _createdAt != value; _createdAt = value; }
        }
        private DateTime _createdAt;

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 5)]
        public virtual DateTime? UpdatedAt
        {
            get { return _updatedAt; }
            set { IsModified |= _updatedAt != value; _updatedAt = value; }
        }
        private DateTime? _updatedAt;

        #endregion
    }
}
