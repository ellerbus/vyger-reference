using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Yapper;
using vyger.Common.Models;
using EnsureThat;

namespace vyger.Common.Repositories
{
    ///	<summary>
    ///
    ///	</summary>
    public interface IWorkoutRoutineExerciseRepository
    {
        void InsertMany(IEnumerable<WorkoutRoutineExercise> routineExercises);

        void DeleteMany(IEnumerable<WorkoutRoutineExercise> routineExercises);

        void UpdateMany(IEnumerable<WorkoutRoutineExercise> routineExercises);

        void Update(WorkoutRoutineExercise exercise);

        WorkoutRoutineExercise SelectOne(int routineId, int weekId, int dayId, int exerciseId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routineId">Required</param>
        /// <param name="weekId">0 to ignore</param>
        /// <param name="dayId">0 to ignore</param>
        /// <param name="exerciseId">0 to ignore</param>
        /// <returns></returns>
        IList<WorkoutRoutineExercise> SelectMany(int routineId, int weekId, int dayId, int exerciseId);
    }

    ///	<summary>
    ///
    ///	</summary>
    class WorkoutRoutineExerciseRepository : IWorkoutRoutineExerciseRepository
    {
        #region Members

        private IDatabaseSession _session;

        #endregion

        #region Constructors

        public WorkoutRoutineExerciseRepository(IDatabaseSession session)
        {
            _session = session;
        }

        #endregion

        #region Methods

        public void InsertMany(IEnumerable<WorkoutRoutineExercise> routineExercises)
        {
            using (IDatabaseTransaction trx = _session.BeginTransaction())
            {
                string sql = StatementBuilder.InsertOne<WorkoutRoutineExercise>();

                _session.Connection.Execute(sql, param: routineExercises, transaction: trx.Transaction);

                trx.Commit();
            }
        }

        public void DeleteMany(IEnumerable<WorkoutRoutineExercise> routineExercises)
        {
            using (IDatabaseTransaction trx = _session.BeginTransaction())
            {
                string sql = StatementBuilder.DeleteOne<WorkoutRoutineExercise>();

                _session.Connection.Execute(sql, param: routineExercises, transaction: trx.Transaction);

                trx.Commit();
            }
        }

        public void UpdateMany(IEnumerable<WorkoutRoutineExercise> routineExercises)
        {
            using (IDatabaseTransaction trx = _session.BeginTransaction())
            {
                string sql = StatementBuilder.UpdateOne<WorkoutRoutineExercise>();

                _session.Connection.Execute(sql, param: routineExercises, transaction: trx.Transaction);

                trx.Commit();
            }
        }

        public void Update(WorkoutRoutineExercise exercise)
        {
            string sql = StatementBuilder.UpdateOne<WorkoutRoutineExercise>();

            _session.Connection.Execute(sql, param: exercise);
        }

        public void Delete(WorkoutRoutineExercise exercise)
        {
            string sql = StatementBuilder.DeleteOne<WorkoutRoutineExercise>();

            _session.Connection.Execute(sql, param: exercise);
        }

        public WorkoutRoutineExercise SelectOne(int routineId, int weekId, int dayId, int exerciseId)
        {
            var item = new
            {
                RoutineId = routineId,
                WeekId = weekId,
                DayId = dayId,
                ExerciseId = exerciseId
            };

            string sql = StatementBuilder.SelectOne<WorkoutRoutineExercise>();

            return _session.Connection
                .Query<WorkoutRoutineExercise>(sql, param: item)
                .Select(row => new WorkoutRoutineExercise(row))
                .FirstOrDefault();
        }

        public IList<WorkoutRoutineExercise> SelectMany(int routineId, int weekId, int dayId, int exerciseId)
        {
            Ensure.That(routineId, nameof(routineId)).IsNot(0);

            IDictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("RoutineId", routineId);

            if (weekId > 0)
            {
                Ensure.That(weekId, nameof(weekId)).IsInRange(Constants.MinWeeks, Constants.MaxWeeks);

                parms.Add("WeekId", weekId);
            }

            if (dayId > 0)
            {
                Ensure.That(dayId, nameof(dayId)).IsInRange(Constants.MinDays, Constants.MaxDays);

                parms.Add("DayId", dayId);
            }

            if (exerciseId > 0)
            {
                parms.Add("ExerciseId", exerciseId);
            }

            object where = parms.ToObject();

            string sql = StatementBuilder.SelectMany<WorkoutRoutineExercise>(where);

            FormattableString fsql = $@"
                order by {nameof(WorkoutRoutineExercise.WeekId):C},
                        {nameof(WorkoutRoutineExercise.DayId):C},
                        {nameof(WorkoutRoutineExercise.SequenceNumber):C}
                ";

            sql += fsql.ToString(SqlFormatter.Default);

            return _session.Connection
                .Query<WorkoutRoutineExercise>(sql, param: where)
                .Select(row => new WorkoutRoutineExercise(row))
                .ToList();
        }

        #endregion
    }
}

