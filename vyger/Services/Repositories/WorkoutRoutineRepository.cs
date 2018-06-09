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
    public interface IWorkoutRoutineRepository
    {
        void Save(WorkoutRoutine routine);

        WorkoutRoutine SelectOne(int routineId);

        IList<WorkoutRoutine> SelectMany(int memberId);
    }

    ///	<summary>
    ///
    ///	</summary>
    class WorkoutRoutineRepository : IWorkoutRoutineRepository
    {
        #region Members

        private IDatabaseSession _session;

        #endregion

        #region Constructors

        public WorkoutRoutineRepository(IDatabaseSession session)
        {
            _session = session;
        }

        #endregion

        #region Methods

        public void Save(WorkoutRoutine routine)
        {
            string sql = StatementBuilder.SaveOne<WorkoutRoutine>();

            routine.RoutineId = _session.Connection
                .Query<int>(sql, param: routine)
                .FirstOrDefault();
        }

        public WorkoutRoutine SelectOne(int routineId)
        {
            var item = new
            {
                RoutineId = routineId
            };

            string sql = StatementBuilder.SelectOne<WorkoutRoutine>();

            return _session.Connection
                .Query<WorkoutRoutine>(sql, param: item)
                .Select(row => new WorkoutRoutine(row))
                .FirstOrDefault();
        }

        public IList<WorkoutRoutine> SelectMany(int memberId)
        {
            var item = new { MemberId = memberId };

            string sql = StatementBuilder.SelectMany<WorkoutRoutine>();

            FormattableString fsql = $@"
                where   {nameof(WorkoutRoutine.OwnerId):C} in (-1, {nameof(Member.MemberId):P})
                ";

            sql += fsql.ToString(SqlFormatter.Default);

            return _session.Connection
                .Query<WorkoutRoutine>(sql, param: item)
                .Select(row => new WorkoutRoutine(row))
                .ToList();
        }

        #endregion
    }
}

