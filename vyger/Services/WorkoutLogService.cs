using CsvHelper.Configuration;
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
        void SaveWorkoutPlans();
    }

    #endregion

    /// <summary>
    /// Service Implementation for WorkoutLog
    /// </summary>
    public class WorkoutLogService : BaseService<WorkoutLog>, IWorkoutLogService
    {
        #region Members

        private class WorkoutLogClassMap : ClassMap<WorkoutLog>
        {
            public WorkoutLogClassMap()
            {
                Map(m => m.LogDate);
                Map(m => m.ExerciseId);
                Map(m => m.Workout);
                Map(m => m.PlanId);
                Map(m => m.CycleId);
                Map(m => m.WeekId);
                Map(m => m.DayId);
                Map(m => m.SequenceNumber);
            }
        }

        private WorkoutLogCollection _logs;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutLogService(
            IExerciseService exercises,
            ISecurityActor actor)
            : base(actor, RepositoryTypes.Csv)
        {
            _logs = new WorkoutLogCollection(exercises.GetExercises(), LoadAll());
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

        public void SaveWorkoutPlans()
        {
            SaveAll(_logs);
        }

        protected override ClassMap<WorkoutLog> GetCsvClassMap()
        {
            return new WorkoutLogClassMap();
        }

        #endregion
    }
}