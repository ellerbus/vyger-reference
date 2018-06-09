using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using vyger.Common.Models;

namespace vyger.Common.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for WorkoutLog
    /// </summary>
    public interface IWorkoutLogService
    {
        /// <summary>
        /// Save Changes (wrapper to DbContext)
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        void AddWorkoutLog(WorkoutLog log);

        /// <summary>
        /// Gets WorkoutLogs
        /// </summary>
        IList<WorkoutLog> GetWorkoutLogs(DateTime date);

        /// <summary>
        /// Gets WorkoutLogs
        /// </summary>
        IList<WorkoutLog> GetWorkoutLogs(int planId, int cycleId);
    }

    #endregion

    /// <summary>
    /// Service Implementation for WorkoutLog
    /// </summary>
    public class WorkoutLogService : IWorkoutLogService
    {
        #region Members

        private IVygerContext _db;
        private ISecurityActor _actor;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutLogService(
            IVygerContext db,
            ISecurityActor actor)
        {
            _db = db;
            _actor = actor;
        }

        #endregion

        #region Methods

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void AddWorkoutLog(WorkoutLog log)
        {
            _db.WorkoutLogs.Add(log);
        }

        /// <summary>
        /// Gets a single WorkoutLog based on the given primary key
        /// </summary>
        public IList<WorkoutLog> GetWorkoutLogs(DateTime logDate)
        {
            IList<WorkoutLog> logs = _db
                .WorkoutLogs
                .Where(x => x.MemberId == _actor.MemberId && x.LogDate == logDate)
                .Include(x => x.Exercise)
                .OrderBy(x => x.SequenceNumber)
                .ThenBy(x => x.Exercise.ExerciseName)
                .ToList();

            return logs;
        }

        /// <summary>
        /// Gets a single WorkoutLog based on the given primary key
        /// </summary>
        public IList<WorkoutLog> GetWorkoutLogs(int planId, int cycleId)
        {
            IList<WorkoutLog> logs = _db
                .WorkoutLogs
                .Where(x => x.MemberId == _actor.MemberId && x.PlanId == planId && x.CycleId == cycleId)
                .Include(x => x.Exercise)
                .OrderBy(x => x.SequenceNumber)
                .ThenBy(x => x.Exercise.ExerciseName)
                .ToList();

            return logs;
        }

        #endregion
    }
}
