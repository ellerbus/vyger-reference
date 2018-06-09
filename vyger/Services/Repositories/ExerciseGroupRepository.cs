using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Yapper;
using vyger.Common.Models;

namespace vyger.Common.Repositories
{
    ///	<summary>
    ///
    ///	</summary>
    public interface IExerciseGroupRepository
    {
        void Save(ExerciseGroup group);

        ExerciseGroup SelectOne(int groupId);

        IList<ExerciseGroup> SelectMany();
    }

    ///	<summary>
    ///
    ///	</summary>
    class ExerciseGroupRepository : IExerciseGroupRepository
    {
        #region Members

        private IDatabaseSession _session;

        #endregion

        #region Constructors

        public ExerciseGroupRepository(IDatabaseSession session)
        {
            _session = session;
        }

        #endregion

        #region Methods

        public void Save(ExerciseGroup group)
        {
            string sql = StatementBuilder.SaveOne<Member>();

            group.GroupId = _session.Connection
                .Query<int>(sql, param: group)
                .FirstOrDefault();
        }

        public ExerciseGroup SelectOne(int groupId)
        {
            var item = new
            {
                GroupId = groupId
            };

            string sql = StatementBuilder.SelectOne<ExerciseGroup>();

            return _session.Connection
                .Query<ExerciseGroup>(sql, param: item)
                .Select(row => new ExerciseGroup(row))
                .FirstOrDefault();
        }

        public IList<ExerciseGroup> SelectMany()
        {
            string sql = StatementBuilder.SelectMany<ExerciseGroup>();

            return _session.Connection
                .Query<ExerciseGroup>(sql)
                .Select(row => new ExerciseGroup(row))
                .ToList();
        }

        #endregion
    }
}

