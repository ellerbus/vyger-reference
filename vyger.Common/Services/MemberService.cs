using System;
using System.Linq;
using EnsureThat;
using vyger.Common.Models;

namespace vyger.Common.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for Member
    /// </summary>
    public interface IMemberService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Gets a single Member based on the given primary key
        /// </summary>
        Member GetMember(int id, SecurityAccess access);

        /// <summary>
        /// Gets a single Member based on the given primary key
        /// </summary>
        Member GetMember(string email, SecurityAccess access);

        /// <summary>
        /// Authenticates a Member
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Member AuthenticateLogin(string token);
    }

    #endregion

    /// <summary>
    /// Service Implementation for Member
    /// </summary>
    public class MemberService : IMemberService
    {
        #region Members

        private IAuthenticationService _authentication;
        private ISecurityActor _actor;
        private IVygerContext _db;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MemberService(
            IAuthenticationService authentication,
            ISecurityActor actor,
            IVygerContext db)
        {
            _authentication = authentication;
            _actor = actor;
            _db = db;
        }

        #endregion

        #region Methods

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        /// <summary>
        /// Gets a single Member based on the given primary key
        /// </summary>
        public Member GetMember(int id, SecurityAccess access)
        {
            Member member = _db.Members.FirstOrDefault(x => x.MemberId == id);

            _actor.VerifyCan(access, member);

            return member;
        }

        /// <summary>
        /// Gets a single Member based on the given primary key
        /// </summary>
        public Member GetMember(string email, SecurityAccess access)
        {
            Ensure.That(email, nameof(email)).IsNotNullOrEmpty();

            Member member = _db.Members.FirstOrDefault(x => x.MemberEmail == email);

            _actor.VerifyCan(access, member);

            return member;
        }

        #endregion

        #region Authentication

        public Member AuthenticateLogin(string token)
        {
            AuthenticationToken verified = _authentication.VerifyGoogleAuthentication(token);

            Member member = GetMember(verified.Email.ToLower(), SecurityAccess.Authenticate);

            if (member == null)
            {
                member = new Member()
                {
                    MemberEmail = verified.Email,
                    Status = StatusTypes.Inactive
                };

                member = _db.Members.Add(member);
            }
            else
            {
                member.UpdatedAt = DateTime.UtcNow;
            }

            SaveChanges();

            return member;
        }

        #endregion
    }
}