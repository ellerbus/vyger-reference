using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Augment;

namespace vyger.Common.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class Member
    {
        #region Constructors

        public Member() : base() { }

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
                string pk = $"[{MemberId}]";

                string uq = $"[{MemberEmail}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("Member", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(Member other)
        {
            MemberEmail = other.MemberEmail;
            IsAdministrator = other.IsAdministrator;
            Status = other.Status;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'member_id'
        ///	</summary>
        [Required]
        [DisplayName("Member Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int MemberId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'member_email'
        ///	</summary>
		[Required]
        [DisplayName("Member Email")]
        public override string MemberEmail
        {
            get { return base.MemberEmail; }
            set { base.MemberEmail = value.AssertNotNull().ToLower(); }
        }

        ///	<summary>
        ///	Gets / Sets database column 'is_administrator'
        ///	</summary>
		[Required]
        [DisplayName("Is Administrator")]
        public override bool IsAdministrator { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[NotMapped]
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
