using System;
using vyger.Common;
using vyger.Models;

namespace vyger.Services
{
    #region Repository interface

    /// <summary>
    /// Repository Interface for Member
    /// </summary>
    public interface IMemberService
    {
        /// <summary>
        /// Gets a single Member based on the given primary key
        /// </summary>
        Member GetMember();

        /// <summary>
        /// 
        /// </summary>
        void SaveMember(Member member);
    }

    #endregion

    /// <summary>
    /// Repository Implementation for Member
    /// </summary>
    public class MemberService : BaseService<Member>, IMemberService
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MemberService(ISecurityActor actor)
            : base(actor)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a single Member based on the given primary key
        /// </summary>
        public Member GetMember()
        {
            Member member = LoadOne();

            return member;
        }

        /// <summary>
        /// Gets a single Member based on the given primary key
        /// </summary>
        public void SaveMember(Member member)
        {
            member.UpdatedAt = DateTime.UtcNow;

            SaveOne(member);
        }

        #endregion
    }
}