using System.Collections.Generic;
using vyger.Common;
using vyger.Models;

namespace vyger.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for WorkoutPlan
    /// </summary>
    public interface IWorkoutLogService
    {
        /// <summary>
        ///
        /// </summary>
        WorkoutLogCollection GetWorkoutLogs();

        /// <summary>
        ///
        /// </summary>
        void SaveWorkoutLogs();
    }

    #endregion

    /// <summary>
    /// Service Implementation for WorkoutLog
    /// </summary>
    public class WorkoutLogService : BaseService, IWorkoutLogService
    {
        #region Members

        private WorkoutLogCollection _logs;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutLogService(
            IExerciseService exercises,
            ISecurityActor actor)
            : base(actor)
        {
            ExerciseCollection ex = exercises.GetExercises();

            IEnumerable<WorkoutLog> logs = ReadData<WorkoutLogCollection>();

            _logs = new WorkoutLogCollection(ex, logs);
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        public WorkoutLogCollection GetWorkoutLogs()
        {
            return _logs;
        }

        public void SaveWorkoutLogs()
        {
            SaveData(_logs);
        }

        #endregion

        #region Properties

        protected override string FileName
        {
            get
            {
                return typeof(WorkoutLog).Name;
            }
        }

        #endregion
    }
}