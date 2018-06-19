using System;
using System.Collections.Generic;
using System.Linq;
using vyger.Common;
using vyger.Models;

namespace vyger.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for WorkoutPlan
    /// </summary>
    public interface IWorkoutPlanService
    {
        /// <summary>
        ///
        /// </summary>
        WorkoutPlanCollection GetWorkoutPlans();

        /// <summary>
        ///
        /// </summary>
        void AddWorkoutPlan(WorkoutPlan plan);

        /// <summary>
        ///
        /// </summary>
        void UpdateWorkoutPlan(string id, WorkoutPlan overlay);

        /// <summary>
        ///
        /// </summary>
        void SaveWorkoutPlans();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        WorkoutPlanCycle CreateCycle(WorkoutPlan plan, IEnumerable<WorkoutLog> logs);
    }

    #endregion

    /// <summary>
    /// Service Implementation for WorkoutPlan
    /// </summary>
    public class WorkoutPlanService : BaseService<WorkoutPlan>, IWorkoutPlanService
    {
        #region Members

        private IWorkoutRoutineService _routines;
        private WorkoutPlanCollection _plans;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutPlanService(
            IWorkoutRoutineService routines,
            ISecurityActor actor)
            : base(actor, RepositoryTypes.Yaml)
        {
            _routines = routines;

            _plans = new WorkoutPlanCollection(LoadAll());

            foreach (WorkoutPlan p in _plans)
            {
                p.Routine = _routines.GetWorkoutRoutines().GetByPrimaryKey(p.RoutineId);

                IEnumerable<WorkoutPlanExercise> exercises = p.Cycles.SelectMany(x => x.PlanExercises);

                foreach (WorkoutPlanExercise pex in exercises)
                {
                    pex.Exercise = p.Routine.AllExercises.GetByPrimaryKey(pex.ExerciseId);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        public WorkoutPlanCollection GetWorkoutPlans()
        {
            return _plans;
        }

        /// <summary>
        ///
        /// </summary>
        public void AddWorkoutPlan(WorkoutPlan add)
        {
            _plans.Add(add);

            SaveWorkoutPlans();
        }

        /// <summary>
        ///
        /// </summary>
        public void UpdateWorkoutPlan(string id, WorkoutPlan overlay)
        {
            WorkoutPlan plan = _plans.GetByPrimaryKey(overlay.Id);

            plan.OverlayWith(overlay);

            SaveWorkoutPlans();
        }

        public void SaveWorkoutPlans()
        {
            SaveAll(_plans);
        }

        #endregion

        #region Generator

        public WorkoutPlanCycle CreateCycle(WorkoutPlan plan, IEnumerable<WorkoutLog> logs)
        {
            WorkoutPlanCycle cycle = new WorkoutPlanCycle()
            {
                CycleId = plan.Cycles.Count + 1,
                CreatedAt = DateTime.UtcNow
            };

            plan.Cycles.Add(cycle);

            WorkoutCycleGenerator generator = new WorkoutCycleGenerator(plan, cycle);

            generator.InitializeCycle(logs.ToList());

            return cycle;
        }

        #endregion
    }
}