using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using vyger.Common.Models;
using Yapper;

namespace vyger.Common.Repositories
{
    ///	<summary>
    ///
    ///	</summary>
    public interface IMemberRepository
    {
        void Save(Member member);

        Member SelectOneById(int memberId);

        Member SelectOneByEmail(string memberEmail);
    }

    ///	<summary>
    ///
    ///	</summary>
    class MemberRepository : IMemberRepository
    {
        #region Members

        private IDatabaseSession _session;

        #endregion

        #region Constructors

        public MemberRepository(IDatabaseSession session)
        {
            _session = session;
        }

        #endregion

        #region Methods

        public void Save(Member member)
        {
            string sql = StatementBuilder.SaveOne<Member>();

            member.MemberId = _session.Connection
                .Query<int>(sql, param: member)
                .FirstOrDefault();
        }

        public Member SelectOneById(int memberId)
        {
            var item = new
            {
                MemberId = memberId
            };

            string sql = StatementBuilder.SelectOne<Member>();

            return _session.Connection
                .Query<Member>(sql, param: item)
                .Select(row => new Member(row))
                .FirstOrDefault();
        }

        public Member SelectOneByEmail(string memberEmail)
        {
            var item = new
            {
                MemberEmail = memberEmail
            };

            string sql = StatementBuilder.SelectOne<Member>(item);

            return _session.Connection
                .Query<Member>(sql, param: item)
                .Select(row => new Member(row))
                .FirstOrDefault();
        }

        #endregion
    }
}

