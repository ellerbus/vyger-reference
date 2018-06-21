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

        /// <summary>
        ///
        /// </summary>
        void GenerateCycle(WorkoutPlan plan, WorkoutPlanCycle cycle, IEnumerable<WorkoutLog> logs);
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

                foreach (WorkoutPlanCycle cycle in p.Cycles)
                {
                    foreach (WorkoutPlanExercise pex in cycle.PlanExercises)
                    {
                        pex.Exercise = p.Routine.AllExercises.GetByPrimaryKey(pex.ExerciseId);
                    }

                    foreach (WorkoutPlanLog pl in cycle.PlanLogs)
                    {
                        pl.Cycle = cycle;
                        pl.PlanExercise = cycle.PlanExercises.GetByPrimaryKey(pl.ExerciseId);
                    }
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

            GenerateCycle(plan, cycle, logs);

            return cycle;
        }

        public void GenerateCycle(WorkoutPlan plan, WorkoutPlanCycle cycle, IEnumerable<WorkoutLog> logs)
        {
            WorkoutCycleGenerator generator = new WorkoutCycleGenerator(plan, cycle);

            cycle.PlanLogs.Clear();

            cycle.PlanLogs.AddRange(generator.GenerateLogItems());
        }

        #endregion
    }
}