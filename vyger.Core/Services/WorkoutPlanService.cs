using System;
using System.Collections.Generic;
using System.Linq;
using Augment;
using Augment.Caching;
using vyger.Core;
using vyger.Core.Models;
using vyger.Core.Repositories;

namespace vyger.Core.Services
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
    public class WorkoutPlanService : IWorkoutPlanService
    {
        #region Members

        private IWorkoutRoutineService _routines;
        private ISecurityActor _actor;
        private IWorkoutPlanRepository _repository;
        private ICacheManager _cache;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutPlanService(
            IWorkoutRoutineService routines,
            ISecurityActor actor,
            IWorkoutPlanRepository repository,
            ICacheManager cache)
        {
            _routines = routines;
            _actor = actor;
            _repository = repository;
            _cache = cache;
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        public WorkoutPlanCollection GetWorkoutPlans()
        {
            WorkoutPlanCollection plans = _cache
                .Cache(() => BuildPlanCollection())
                .By("Actor", _actor.Email)
                .DurationOf(5.Minutes())
                .CachedObject;

            return plans;
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutPlanCollection BuildPlanCollection()
        {
            WorkoutRoutineCollection r = _routines.GetWorkoutRoutines();

            IEnumerable<WorkoutPlan> plans = _repository.GetWorkoutPlans();

            return new WorkoutPlanCollection(r, plans);
        }

        /// <summary>
        ///
        /// </summary>
        public void AddWorkoutPlan(WorkoutPlan add)
        {
            WorkoutPlanCollection plans = GetWorkoutPlans();

            plans.Add(add);

            SaveWorkoutPlans();
        }

        /// <summary>
        ///
        /// </summary>
        public void UpdateWorkoutPlan(string id, WorkoutPlan overlay)
        {
            WorkoutPlanCollection plans = GetWorkoutPlans();

            WorkoutPlan plan = plans.GetByPrimaryKey(overlay.Id);

            plan.OverlayWith(overlay);

            SaveWorkoutPlans();
        }

        public void SaveWorkoutPlans()
        {
            WorkoutPlanCollection plans = GetWorkoutPlans();

            _repository.SaveWorkoutPlans(plans);
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

            if (logs.Count() > 0)
            {
                WorkoutCycleGenerator generator = new WorkoutCycleGenerator(plan, cycle);

                generator.InitializeCycle(logs.ToList());

                GenerateCycle(plan, cycle, logs);
            }

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