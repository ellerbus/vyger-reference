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
    public interface IExerciseRepository
    {
        void Save(Exercise exercise);

        Exercise SelectOne(int exerciseId);

        IList<Exercise> SelectMany(int groupId, int memberId);
    }

    ///	<summary>
    ///
    ///	</summary>
    class ExerciseRepository : IExerciseRepository
    {
        #region Members

        private IDatabaseSession _session;

        #endregion

        #region Constructors

        public ExerciseRepository(IDatabaseSession session)
        {
            _session = session;
        }

        #endregion

        #region Methods

        public void Save(Exercise exercise)
        {
            string sql = StatementBuilder.SaveOne<Exercise>();

            exercise.ExerciseId = _session.Connection
                .Query<int>(sql, param: exercise)
                .FirstOrDefault();
        }

        public Exercise SelectOne(int exerciseId)
        {
            var item = new
            {
                ExerciseId = exerciseId
            };

            string sql = StatementBuilder.SelectOne<Exercise>();

            return _session.Connection
                .Query<Exercise>(sql, param: item)
                .Select(row => new Exercise(row))
                .FirstOrDefault();
        }

        public IList<Exercise> SelectMany(int groupId, int memberId)
        {
            var item = new { MemberId = memberId, GroupId = groupId };

            string sql = StatementBuilder.SelectMany<Exercise>();

            FormattableString fsql = $@"
                where   {nameof(Exercise.OwnerId):C} in (-1, {nameof(Member.MemberId):P})
                  and   (
                            {nameof(Exercise.GroupId):C} = {nameof(Exercise.GroupId):P}
                            or
                            {nameof(Exercise.GroupId):P} = 0
                        )
                ";

            sql += fsql.ToString(SqlFormatter.Default);

            return _session.Connection
                .Query<Exercise>(sql, param: item)
                .Select(row => new Exercise(row))
                .ToList();
        }

        #endregion
    }
}

