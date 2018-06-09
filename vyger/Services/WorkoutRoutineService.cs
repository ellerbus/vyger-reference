using System.Collections.Generic;
using vyger.Common.Collections;
using vyger.Common.Models;
using vyger.Common.Repositories;

namespace vyger.Common.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for WorkoutRoutine
    /// </summary>
    public interface IWorkoutRoutineService
    {
        /// <summary>
        /// Gets a single WorkoutRoutine based on the given primary key
        /// </summary>
        WorkoutRoutineCollection GetWorkoutRoutines();

        /// <summary>
        /// Gets a single WorkoutRoutine based on the given primary key
        /// </summary>
        WorkoutRoutine GetWorkoutRoutine(int routineId);

        /// <summary>
        /// 
        /// </summary>
        WorkoutRoutine SaveWorkoutRoutine(WorkoutRoutine routine);
    }

    #endregion

    /// <summary>
    /// Service Implementation for WorkoutRoutine
    /// </summary>
    public class WorkoutRoutineService : IWorkoutRoutineService
    {
        #region Members

        private IWorkoutRoutineRepository _repository;
        private ISecurityActor _actor;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutRoutineService(
            IWorkoutRoutineRepository repository,
            ISecurityActor actor)
        {
            _repository = repository;
            _actor = actor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a single WorkoutRoutine based on the given primary key
        /// </summary>
        public WorkoutRoutineCollection GetWorkoutRoutines()
        {
            IList<WorkoutRoutine> list = _repository.SelectMany(_actor.MemberId);

            WorkoutRoutineCollection routines = new WorkoutRoutineCollection(list);

            return routines;
        }

        /// <summary>
        /// Gets a single WorkoutRoutine based on the given primary key
        /// </summary>
        public WorkoutRoutine GetWorkoutRoutine(int routineId)
        {
            WorkoutRoutine routine = _repository.SelectOne(routineId);

            _actor.VerifyCan(SecurityAccess.View, routine);

            return routine;
        }

        /// <summary>
        /// Saves a WorkoutRoutine
        /// </summary>
        /// <returns></returns>
        public WorkoutRoutine SaveWorkoutRoutine(WorkoutRoutine temp)
        {
            _actor.VerifyCan(SecurityAccess.Update, temp);

            WorkoutRoutine routine = OverlayCache(temp);

            if (routine.IsModified)
            {
                _actor.EnsureAudit(routine);

                _repository.Save(routine);
            }

            return routine;
        }

        /// <summary>
        /// OverlayCache for WorkoutRoutine
        /// </summary>
        /// <returns></returns>
        private WorkoutRoutine OverlayCache(WorkoutRoutine temp)
        {
            WorkoutRoutine routine = GetWorkoutRoutine(temp.RoutineId);

            if (routine == null)
            {
                routine = temp;
            }
            else
            {
                routine.OverlayWith(temp);
            }

            return routine;
        }

        #endregion
    }
}
