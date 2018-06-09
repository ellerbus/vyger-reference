using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Augment;
using vyger.Common.Models;

namespace vyger.Common.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for WorkoutPlan
    /// </summary>
    public interface IWorkoutPlanService
    {
        /// <summary>
        /// Save Changes (wrapper to DbContext)
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Gets a single WorkoutPlan based on the given primary key
        /// </summary>
        IList<WorkoutPlan> GetWorkoutPlans();

        /// <summary>
        /// Gets a single WorkoutPlan based on the given primary key
        /// </summary>
        WorkoutPlan GetWorkoutPlan(int planId, SecurityAccess access);

        /// <summary>
        /// Gets a single WorkoutPlan based on the given primary key
        /// </summary>
        WorkoutPlan GetWorkoutPlanWithCycles(int planId, SecurityAccess access);

        /// <summary>
        /// Gets a single WorkoutPlan based on the given primary key
        /// </summary>
        WorkoutPlanCycle GetWorkoutPlanCycle(int planId, int cycleId, SecurityAccess access);

        /// <summary>
        /// Save Changes (wrapper to DbContext)
        /// </summary>
        /// <returns></returns>
        WorkoutPlan AddWorkoutPlan(WorkoutPlan plan);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="planId"></param>
        WorkoutPlanCycle AddWorkoutCycle(int planId);

        /// <summary>
        /// 
        /// </summary>
        void GenerateCycle(int planId, int cycleId, IList<WorkoutPlanExercise> planExercises);
    }

    #endregion

    /// <summary>
    /// Service Implementation for WorkoutPlan
    /// </summary>
    public class WorkoutPlanService : IWorkoutPlanService
    {
        #region Members

        private IVygerContext _db;
        private ISecurityActor _actor;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutPlanService(
            IVygerContext db,
            ISecurityActor actor)
        {
            _db = db;
            _actor = actor;
        }

        #endregion

        #region Getter Methods

        /// <summary>
        /// Gets a single WorkoutPlan based on the given primary key
        /// </summary>
        public IList<WorkoutPlan> GetWorkoutPlans()
        {
            IList<WorkoutPlan> plans = _db
                .WorkoutPlans
                .WithAccessFor(_actor)
                .Include(x => x.Routine)
                .ToList();

            return plans;
        }

        /// <summary>
        /// Gets a single WorkoutPlan based on the given primary key
        /// </summary>
        public WorkoutPlan GetWorkoutPlan(int planId, SecurityAccess access)
        {
            WorkoutPlan plan = _db
                .WorkoutPlans
                .WithAccessFor(_actor)
                .Where(x => x.PlanId == planId)
                .Include(x => x.Routine)
                .FirstOrDefault();

            _actor.VerifyCan(access, plan);

            return plan;
        }

        /// <summary>
        /// Gets a single WorkoutPlan based on the given primary key
        /// </summary>
        public WorkoutPlan GetWorkoutPlanWithCycles(int planId, SecurityAccess access)
        {
            WorkoutPlan plan = GetWorkoutPlan(planId, access);

            plan.Cycles = _db
                .WorkoutPlans
                .SelectMany(x => x.Cycles)
                .Where(x => x.PlanId == planId)
                .ToList();

            return plan;
        }

        /// <summary>
        /// Gets a single WorkoutPlan based on the given primary key
        /// </summary>
        public WorkoutPlanCycle GetWorkoutPlanCycle(int planId, int cycleId, SecurityAccess access)
        {
            WorkoutPlan plan = GetWorkoutPlan(planId, access);

            WorkoutPlanCycle cycle = _db
                .WorkoutPlans
                .SelectMany(x => x.Cycles)
                .Where(x => x.PlanId == planId && x.CycleId == cycleId)
                .Include(x => x.PlanExercises)
                .Include(x => x.PlanExercises.Select(y => y.Exercise))
                .Include(x => x.PlanExercises.Select(y => y.Exercise).Select(z => z.Group))
                .Include(x => x.PlanExercises.Select(y => y.PlanLogs))
                .Include(x => x.PlanExercises.Select(y => y.PlanLogs.Select(z => z.Exercise)))
                .FirstOrDefault();

            cycle.Plan = plan;

            return cycle;
        }

        private void AttachWorkoutRoutine(WorkoutPlan plan)
        {
            plan.Routine = _db
                .WorkoutRoutines
                .Where(x => x.RoutineId == plan.RoutineId)
                .Include(x => x.RoutineExercises)
                .FirstOrDefault();
        }

        #endregion

        #region Saver Methods

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public WorkoutPlan AddWorkoutPlan(WorkoutPlan plan)
        {
            //  inactive previous plans

            string active = StatusTypes.Active.ToString();

            IList<WorkoutPlan> activePlans = _db.WorkoutPlans
                .WithAccessFor(_actor)
                .Where(x => x.StatusEnum == active)
                .ToList();

            foreach (WorkoutPlan activePlan in activePlans)
            {
                activePlan.Status = StatusTypes.Inactive;
            }

            plan.OwnerId = _actor.MemberId;
            plan.Status = StatusTypes.Active;

            WorkoutPlan added = _db.WorkoutPlans.Add(plan);

            AddWorkoutCycle(added);

            SaveChanges();

            return added;
        }

        public WorkoutPlanCycle AddWorkoutCycle(int planId)
        {
            WorkoutPlan plan = GetWorkoutPlan(planId, SecurityAccess.Update);

            plan.CycleId += 1;

            return AddWorkoutCycle(plan);
        }

        private WorkoutPlanCycle AddWorkoutCycle(WorkoutPlan plan)
        {
            plan.Cycles = _db
                .WorkoutPlans
                .SelectMany(x => x.Cycles)
                .Where(x => x.PlanId == plan.PlanId && x.CycleId == plan.CycleId - 1)
                .Include(x => x.PlanExercises)
                .ToList();

            AttachWorkoutRoutine(plan);

            WorkoutPlanCycle cycle = new WorkoutPlanCycle()
            {
                CycleId = plan.CycleId
            };

            plan.Cycles.Add(cycle);

            IList<WorkoutLog> logs = null;

            if (cycle.CycleId == 1)
            {
                DateTime date = DateTime.Today.AddMonths(-3).BeginningOfMonth();

                logs = _db
                    .WorkoutLogs
                    .Where(x => x.MemberId == _actor.MemberId && x.LogDate >= date)
                    .ToList();
            }
            else
            {
                foreach (WorkoutPlanCycle prev in plan.Cycles.Where(x => x.CycleId < plan.CycleId))
                {
                    prev.Status = StatusTypes.Inactive;
                }

                logs = _db
                    .WorkoutLogs
                    .Where(x => x.MemberId == _actor.MemberId && x.PlanId == plan.PlanId && x.CycleId == plan.CycleId - 1)
                    .ToList();
            }

            WorkoutCycleGenerator generator = new WorkoutCycleGenerator(plan, cycle);

            generator.InitializeCycle(logs);

            SaveChanges();

            return cycle;
        }

        public void GenerateCycle(int planId, int cycleId, IList<WorkoutPlanExercise> planExercises)
        {
            WorkoutPlan plan = GetWorkoutPlan(planId, SecurityAccess.Update);

            AttachWorkoutRoutine(plan);

            WorkoutPlanCycle cycle = _db
                .WorkoutPlans
                .SelectMany(x => x.Cycles)
                .Where(x => x.PlanId == planId && x.CycleId == cycleId)
                .Include(x => x.PlanExercises)
                .Include(x => x.PlanExercises.Select(y => y.PlanLogs))
                .FirstOrDefault();

            foreach (WorkoutPlanExercise source in planExercises)
            {
                //  find all matching exercises
                WorkoutPlanExercise target = cycle
                    .PlanExercises
                    .FirstOrDefault(x => x.CycleId == source.CycleId && x.ExerciseId == source.ExerciseId);

                target.OverlayWith(source);
            }

            WorkoutCycleGenerator gen = new WorkoutCycleGenerator(plan, cycle);

            foreach (WorkoutPlanLog log in gen.GenerateLogItems())
            {
                WorkoutPlanExercise pex = cycle.PlanExercises.FirstOrDefault(x => x.ExerciseId == log.ExerciseId);

                WorkoutPlanLog target = pex
                    .PlanLogs
                    .FirstOrDefault(x => x.WeekId == log.WeekId && x.DayId == log.DayId);

                if (target == null)
                {
                    pex.PlanLogs.Add(log);
                }
                else
                {
                    target.OverlayWith(log);
                }
            }

            SaveChanges();
        }

        #endregion
    }
}
