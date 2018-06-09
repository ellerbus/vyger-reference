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
    public partial class ExerciseGroup
    {
        #region Constructors

        public ExerciseGroup() { }

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
                string pk = $"[{GroupId}]";

                string uq = $"[{GroupName}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("ExerciseGroup", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(ExerciseGroup other)
        {
            GroupName = other.GroupName;
            Status = other.Status;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'group_id'
        ///	</summary>
        [Required]
        [DisplayName("Group Id")]
        public override int GroupId
        {
            get { return base.GroupId; }
            set { base.GroupId = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'group_name'
        ///	</summary>
		[Required]
        [DisplayName("Group Name")]
        public override string GroupName
        {
            get { return base.GroupName; }
            set { base.GroupName = value; }
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

        #endregion
    }
}
