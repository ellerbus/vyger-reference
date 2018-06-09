using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Augment;

namespace vyger.Common.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class Exercise
    {
        #region Constructors

        public Exercise() { }

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
                string pk = $"[{ExerciseId}]";

                string uq = $"[{OwnerId}, {ExerciseName}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("Exercise", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(Exercise other)
        {
            ExerciseName = other.ExerciseName;
            GroupId = other.GroupId;
            Status = other.Status;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
        [Required]
        [DisplayName("Exercise Id")]
        public override int ExerciseId
        {
            get { return base.ExerciseId; }
            set { base.ExerciseId = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'owner_id'
        ///	</summary>
		[Required]
        [DisplayName("Owner Id")]
        public override int OwnerId
        {
            get { return base.OwnerId; }
            set { base.OwnerId = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_name'
        ///	</summary>
		[Required]
        [DisplayName("Exercise Name")]
        public override string ExerciseName
        {
            get { return base.ExerciseName; }
            set { base.ExerciseName = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'group_id'
        ///	</summary>
		[Required]
        [DisplayName("Group Id")]
        public override int GroupId
        {
            get { return Group == null ? base.GroupId : Group.GroupId; }
            set { base.GroupId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
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
        ///	Gets / Sets the foreign key to 'group_id'
        ///	</summary>
        public ExerciseGroup Group { get; set; }

        #endregion
    }
}
