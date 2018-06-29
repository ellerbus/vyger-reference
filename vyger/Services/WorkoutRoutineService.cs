using System.Collections.Generic;
using vyger.Common;
using vyger.Models;

namespace vyger.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for WorkoutRoutine
    /// </summary>
    public interface IWorkoutRoutineService
    {
        /// <summary>
        ///
        /// </summary>
        WorkoutRoutineCollection GetWorkoutRoutines();

        /// <summary>
        ///
        /// </summary>
        void AddWorkoutRoutine(WorkoutRoutine routine);

        /// <summary>
        ///
        /// </summary>
        void UpdateWorkoutRoutine(string id, WorkoutRoutine overlay);

        /// <summary>
        ///
        /// </summary>
        void SaveWorkoutRoutines();
    }

    #endregion

    /// <summary>
    /// Service Implementation for WorkoutRoutine
    /// </summary>
    public class WorkoutRoutineService : BaseService, IWorkoutRoutineService
    {
        #region Members

        private WorkoutRoutineCollection _routines;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutRoutineService(
            IExerciseService exercises,
            ISecurityActor actor)
            : base(actor)
        {
            ExerciseCollection ex = exercises.GetExercises();

            IEnumerable<WorkoutRoutine> routines = ReadData<WorkoutRoutineCollection>();

            _routines = new WorkoutRoutineCollection(ex, routines);
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        public WorkoutRoutineCollection GetWorkoutRoutines()
        {
            return _routines;
        }

        /// <summary>
        ///
        /// </summary>
        public void AddWorkoutRoutine(WorkoutRoutine add)
        {
            _routines.Add(add);

            SaveWorkoutRoutines();
        }

        /// <summary>
        ///
        /// </summary>
        public void UpdateWorkoutRoutine(string id, WorkoutRoutine overlay)
        {
            WorkoutRoutine routine = _routines.GetByPrimaryKey(overlay.Id);

            routine.OverlayWith(overlay);

            SaveWorkoutRoutines();
        }

        public void SaveWorkoutRoutines()
        {
            SaveData(_routines);
        }

        #endregion

        #region Properties

        protected override string FileName
        {
            get
            {
                return typeof(WorkoutRoutine).Name;
            }
        }

        #endregion
    }
}