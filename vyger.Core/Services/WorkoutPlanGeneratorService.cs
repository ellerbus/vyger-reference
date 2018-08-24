using System;
using System.Collections.Generic;
using System.Linq;
using vyger.Core.Models;

namespace vyger.Core.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for Exercise
    /// </summary>
    public interface IPlanGeneratorService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        WorkoutPlan CreateWorkoutPlan(WorkoutRoutine routine);
    }

    #endregion

    /// <summary>
    /// Service Implementation for Exercise
    /// </summary>
    public class WorkoutPlanGeneratorService : IPlanGeneratorService
    {
        #region Members

        private IWorkoutLogService _logs;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutPlanGeneratorService(IWorkoutLogService logs)
        {
            _logs = logs;
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        public WorkoutPlan CreateWorkoutPlan(WorkoutRoutine routine)
        {
            foreach (WorkoutPlan p in routine.Plans)
            {
                p.Status = StatusTypes.Inactive;
            }

            WorkoutPlan plan = new WorkoutPlan()
            {
                Id = routine.Plans.Count + 1,
                Routine = routine,
                Status = StatusTypes.Active,
                CreatedAt = DateTime.UtcNow
            };

            CreateWorkoutCycle(plan);

            return plan;
        }

        /// <summary>
        ///
        /// </summary>
        public void CreateWorkoutCycle(WorkoutPlan plan)
        {
            int nextCycleId = plan.CurrentCycleId + 1;

            IList<WorkoutLog> historicalLogs = GetHistoricalLogs(plan).ToList();

            LoadWorkoutCycle(plan, nextCycleId, historicalLogs);
        }

        private IEnumerable<WorkoutLog> GetHistoricalLogs(WorkoutPlan plan)
        {
            if (plan.CurrentCycleId == 0)
            {
                //  new cycle - get last 90 days
                return _logs
                    .GetWorkoutLogs()
                    .Filter(DateTime.Today.AddDays(-90), DateTime.Today);
            }

            return _logs
                .GetWorkoutLogs()
                .Filter(plan.Routine.Id, plan.Id, plan.CurrentCycleId);
        }

        private void LoadWorkoutCycle(WorkoutPlan plan, int cycleId, IList<WorkoutLog> logs)
        {
            HashSet<string> exercises = new HashSet<string>();

            IEnumerable<WorkoutRoutineExercise> routineExercises = plan.Routine.RoutineExercises;

            foreach (WorkoutRoutineExercise routineExercise in routineExercises)
            {
                if (!exercises.Contains(routineExercise.ExerciseId))
                {
                    bool calculated = plan.Routine.RoutineExercises
                        .Where(x => x.ExerciseId == routineExercise.ExerciseId)
                        .SelectMany(x => x.Sets)
                        .Any(x => x.IsRepMax);

                    WorkoutPlanCycle planCycle = new WorkoutPlanCycle()
                    {
                        CycleId = cycleId,
                        ExerciseId = routineExercise.ExerciseId,
                        IsCalculated = calculated
                    };

                    exercises.Add(routineExercise.ExerciseId);

                    plan.PlanCycles.Add(planCycle);

                    MergePreviousCycle(plan, cycleId, planCycle);

                    CreateCycleGoal(plan, cycleId, planCycle, logs);
                }
            }
        }

        private void MergePreviousCycle(WorkoutPlan plan, int cycleId, WorkoutPlanCycle planCycle)
        {
            WorkoutPlanCycle previous = plan.PlanCycles.Filter(cycleId - 1, planCycle.ExerciseId).FirstOrDefault();

            if (previous != null && previous.IsCalculated)
            {
                planCycle.Weight = previous.Weight;
                planCycle.Reps = previous.Reps;
            }
        }

        private void CreateCycleGoal(WorkoutPlan plan, int cycleId, WorkoutPlanCycle planCycle, IList<WorkoutLog> previousCycleLogs)
        {
            WorkoutLog max = null;

            //  within the history find the best 1RM
            foreach (WorkoutLog log in previousCycleLogs.Where(x => x.ExerciseId == planCycle.ExerciseId))
            {
                if (max == null || log.OneRepMax > max.OneRepMax)
                {
                    max = log;
                }
            }

            if (max != null)
            {
                WorkoutLogSet maxSet = max.Sets.GetOneRepMax();

                if (planCycle.Weight > 0)
                {
                    if (maxSet.OneRepMax > planCycle.OneRepMax)
                    {
                        //  increased =-> new goal to target
                        planCycle.Weight = maxSet.Weight;
                        planCycle.Reps = maxSet.Reps;
                    }
                    else if (maxSet.OneRepMax == planCycle.OneRepMax)
                    {
                        //  stagnent - pullback 5%
                        planCycle.Weight = maxSet.Weight;
                        planCycle.Reps = maxSet.Reps;
                        planCycle.Pullback = 5;
                    }
                    else
                    {
                        //  never hit the target - pullback 10%
                        planCycle.Pullback = 10;
                    }
                }
                else if (cycleId == 1)
                {
                    //  obviously no previous target
                    //  take the best and pullback 10%
                    planCycle.Weight = maxSet.Weight;
                    planCycle.Reps = maxSet.Reps;
                    planCycle.Pullback = 10;
                }
            }
        }

        #endregion
    }
}