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
    public partial class Member : Entities.MemberEntity
    {
        #region Constructors

        public Member(int memberId, string memberEmail, bool isAdministrator, string statusEnum, DateTime createdAt, DateTime? updatedAt)
            : base(memberId, memberEmail, isAdministrator, statusEnum, createdAt, updatedAt)
        {
        }
        
        public Member(Member member)
			: this(member.MemberId, member.MemberEmail, member.IsAdministrator, member.StatusEnum, member.CreatedAt, member.UpdatedAt)
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
    [Table("Member")]
    public abstract class MemberEntity
    {
        #region Constructors

        protected MemberEntity() { }

        protected MemberEntity(int memberId, string memberEmail, bool isAdministrator, string statusEnum, DateTime createdAt, DateTime? updatedAt)
        {
            MemberId = memberId;
            MemberEmail = memberEmail;
            IsAdministrator = isAdministrator;
            StatusEnum = statusEnum;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        
        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'member_id'
        ///	</summary>
		[Key]
        [Column("member_id", Order = 1)]
        public virtual int MemberId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'member_email'
        ///	</summary>
		[MaxLength(120)]
        [Column("member_email", Order = 2)]
        public virtual string MemberEmail { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'is_administrator'
        ///	</summary>
        [Column("is_administrator", Order = 3)]
        public virtual bool IsAdministrator { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'status_enum'
        ///	</summary>
		[MaxLength(15)]
        [Column("status_enum", Order = 4)]
        public virtual string StatusEnum { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'created_at'
        ///	</summary>
        [Column("created_at", Order = 5)]
        public virtual DateTime CreatedAt { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'updated_at'
        ///	</summary>
        [Column("updated_at", Order = 6)]
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}
