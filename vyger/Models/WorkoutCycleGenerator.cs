using System;
using System.Collections.Generic;
using System.Linq;

namespace vyger.Models
{
    public class WorkoutCycleGenerator
    {
        #region Members

        private class CycleReference
        {
            public CycleReference(WorkoutPlanExercise planExercise, WorkoutRoutineExercise routineExercise)
            {
                PlanExercise = planExercise;
                RoutineExercise = routineExercise;

                Sets = new WorkoutPlanSetCollection();

                if (RoutineExercise != null)
                {
                    foreach (WorkoutRoutineSet set in RoutineExercise.Sets)
                    {
                        Sets.Add(new WorkoutPlanSet(planExercise, routineExercise, set));
                    }
                }
            }

            public WorkoutPlanLog CreateLogItem()
            {
                return new WorkoutPlanLog()
                {
                    ExerciseId = PlanExercise.ExerciseId,
                    WeekId = WeekId,
                    DayId = DayId,
                    WorkoutPlan = Sets.Display,
                    SequenceNumber = RoutineExercise.SequenceNumber
                };
            }

            public WorkoutPlanExercise PlanExercise { get; private set; }

            public WorkoutRoutineExercise RoutineExercise { get; private set; }

            public WorkoutPlanSetCollection Sets { get; private set; }

            public int WeekId
            {
                get { return RoutineExercise.WeekId; }
            }

            public int DayId
            {
                get { return RoutineExercise.DayId; }
            }
        }

        #endregion

        #region Constructor

        public WorkoutCycleGenerator(WorkoutPlan plan, WorkoutPlanCycle cycle)
        {
            Plan = plan;
            Cycle = cycle;
        }

        #endregion

        #region Initialize Methods

        public void InitializeCycle(IList<WorkoutLog> logs)
        {
            HashSet<string> exerciseIds = new HashSet<string>();

            foreach (WorkoutRoutineExercise rex in Plan.Routine.RoutineExercises)
            {
                //  looking for a distinct list of exercises from the routine
                //
                if (!exerciseIds.Contains(rex.ExerciseId))
                {
                    WorkoutPlanExercise exercise = GetWorkoutPlanExercise(rex.Exercise);

                    if (exercise.Cycle.CycleId > 1)
                    {
                        MergePreviousCycle(exercise);
                    }

                    if (exercise.IsCalculated)
                    {
                        SetupGoal(exercise, logs);
                    }

                    exerciseIds.Add(rex.ExerciseId);
                }
            }
        }

        private WorkoutPlanExercise GetWorkoutPlanExercise(Exercise exercise)
        {
            //  is this static or requires calculations from an "RM"
            bool requiresOneRepMax = Plan.Routine.RoutineExercises
                .Where(x => x.ExerciseId == exercise.Id)
                .SelectMany(x => x.Sets)
                .Any(x => x.IsRepMax);

            WorkoutPlanExercise pex = null;

            if (Cycle.PlanExercises.ContainsPrimaryKey(exercise.Id))
            {
                pex = Cycle.PlanExercises.GetByPrimaryKey(exercise.Id);
            }
            else
            {
                pex = new WorkoutPlanExercise()
                {
                    Cycle = Cycle,
                    ExerciseId = exercise.Id,
                    IsCalculated = requiresOneRepMax
                };

                Cycle.PlanExercises.Add(pex);
            }

            return pex;
        }

        private void MergePreviousCycle(WorkoutPlanExercise current)
        {
            WorkoutPlanExercise previous = Plan
                .Cycles
                .SelectMany(x => x.PlanExercises)
                .FirstOrDefault(x => x.Cycle.CycleId == current.Cycle.CycleId - 1 && x.ExerciseId == current.ExerciseId);

            if (previous != null && previous.IsCalculated)
            {
                current.Weight = previous.Weight;
                current.Reps = previous.Reps;
            }
        }

        private void SetupGoal(WorkoutPlanExercise current, IList<WorkoutLog> logs)
        {
            WorkoutLog max = null;

            foreach (WorkoutLog log in logs.Where(x => x.ExerciseId == current.ExerciseId))
            {
                if (max == null || log.OneRepMax > max.OneRepMax)
                {
                    max = log;
                }
            }

            if (max != null)
            {
                WorkoutLogSet set = max.Sets.GetOneRepMax();

                if (current.Weight > 0)
                {
                    if (set.OneRepMax > current.OneRepMax)
                    {
                        //  increased =-> new goal to target
                        current.Weight = set.Weight;
                        current.Reps = set.Reps;
                    }
                    else if (set.OneRepMax == current.OneRepMax)
                    {
                        //  stagnent - pullback 5%
                        current.Weight = set.Weight;
                        current.Reps = set.Reps;
                        current.Pullback = 5;
                    }
                    else
                    {
                        //  never hit the target - pullback 10%
                        current.Pullback = 10;
                    }
                }
                else if (current.Cycle.CycleId == 1)
                {
                    //  obviously no previous target
                    //  take the best and pullback 10%
                    current.Weight = set.Weight;
                    current.Reps = set.Reps;
                    current.Pullback = 10;
                }
            }
        }

        #endregion

        #region Log Generation Methods

        public IEnumerable<WorkoutPlanLog> GenerateLogItems()
        {
            foreach (WorkoutPlanExercise exercise in Cycle.PlanExercises)
            {
                //  foreach exercise shred out the calculations
                foreach (WorkoutPlanLog log in GenerateLogItems(exercise))
                {
                    yield return log;
                }
            }
        }

        private IEnumerable<WorkoutPlanLog> GenerateLogItems(WorkoutPlanExercise exercise)
        {
            CycleReference[,] references = GenerateReferenceGrid(exercise);

            //  calculate same day set-refs first
            CalculateSameDayReferences(references);

            //  calculate same week day&set-refs first
            CalculateSameWeekReferences(references);

            //  finally get the other week references
            CalculateCycleReferences(references);

            for (int w = 0; w < TotalWeeks; w++)
            {
                for (int d = 0; d < TotalDays; d++)
                {
                    CycleReference reference = references[w, d];

                    if (reference.RoutineExercise != null)
                    {
                        //  if something is missed - the formulas are dorked
                        if (reference.Sets.Any(x => x.IsBuildingPlan && double.IsNaN(x.CalculatedWeight)))
                        {
                            throw new InvalidOperationException("A formula is dorked - TODO - Validate");
                        }

                        WorkoutPlanLog log = reference.CreateLogItem();

                        yield return log;
                    }
                }
            }
        }

        private void CalculateStaticReferences(CycleReference[,] references)
        {
            for (int w = 0; w < TotalWeeks; w++)
            {
                for (int d = 0; d < TotalDays; d++)
                {
                    foreach (WorkoutPlanSet set in references[w, d].Sets)
                    {
                        if (set.RoutineSet.IsStatic && double.IsNaN(set.CalculatedWeight))
                        {
                            set.CalculatedWeight = set.RoutineSet.Weight;
                        }
                    }
                }
            }
        }

        private void CalculateSameDayReferences(CycleReference[,] references)
        {
            for (int w = 0; w < TotalWeeks; w++)
            {
                for (int d = 0; d < TotalDays; d++)
                {
                    int lastSetIndex = references[w, d].Sets.Count - 1;

                    foreach (WorkoutPlanSet set in references[w, d].Sets)
                    {
                        if (set.RoutineSet.IsReference && double.IsNaN(set.CalculatedWeight))
                        {
                            if (set.RoutineSet.WeekId == 0 && set.RoutineSet.DayId == 0)
                            {
                                int setIndex = GetSetIndex(set, lastSetIndex);

                                WorkoutPlanSet reference = references[w, d].Sets[setIndex];

                                set.CalculatedWeight = reference.CalculatedWeight * set.RoutineSet.Percent;
                            }
                        }
                    }
                }
            }
        }

        private void CalculateSameWeekReferences(CycleReference[,] references)
        {
            for (int w = 0; w < TotalWeeks; w++)
            {
                int lastDayIndex = TotalDays - 1;

                for (int d = 0; d < TotalDays; d++)
                {
                    foreach (WorkoutPlanSet set in references[w, d].Sets)
                    {
                        if (set.RoutineSet.IsReference && double.IsNaN(set.CalculatedWeight))
                        {
                            if (set.RoutineSet.WeekId == 0)
                            {
                                int dayIndex = GetDayIndex(set, lastDayIndex);

                                int lastSetIndex = references[w, dayIndex].Sets.Count - 1;

                                int setIndex = GetSetIndex(set, lastSetIndex);

                                WorkoutPlanSet reference = references[w, dayIndex].Sets[setIndex];

                                set.CalculatedWeight = reference.CalculatedWeight * set.RoutineSet.Percent;
                            }
                        }
                    }
                }
            }
        }

        private void CalculateCycleReferences(CycleReference[,] references)
        {
            int lastWeekIndex = TotalWeeks - 1;

            for (int w = 0; w < TotalWeeks; w++)
            {
                int lastDayIndex = TotalDays - 1;

                for (int d = 0; d < TotalDays; d++)
                {
                    foreach (WorkoutPlanSet set in references[w, d].Sets)
                    {
                        if (set.RoutineSet.IsReference && double.IsNaN(set.CalculatedWeight))
                        {
                            int weekIndex = GetWeekIndex(set, lastWeekIndex);
                            int dayIndex = GetDayIndex(set, lastDayIndex);

                            int lastSetIndex = references[weekIndex, dayIndex].Sets.Count - 1;

                            int setIndex = GetSetIndex(set, lastSetIndex);

                            WorkoutPlanSet reference = references[weekIndex, dayIndex].Sets[setIndex];

                            set.CalculatedWeight = reference.CalculatedWeight * set.RoutineSet.Percent;
                        }
                    }
                }
            }
        }

        private int GetWeekIndex(WorkoutPlanSet set, int lastWeekIndex)
        {
            int weekId = set.RoutineSet.WeekId;

            int weekIndex = weekId == -1 ? lastWeekIndex : weekId - 1;

            return weekIndex;
        }

        private int GetDayIndex(WorkoutPlanSet set, int lastDayIndex)
        {
            int dayId = set.RoutineSet.DayId;

            int dayIndex = dayId == -1 ? lastDayIndex : dayId - 1;

            return dayIndex;
        }

        private int GetSetIndex(WorkoutPlanSet set, int lastSetIndex)
        {
            int setId = set.RoutineSet.SetId;

            int setIndex = setId == -1 ? lastSetIndex : setId - 1;

            return setIndex;
        }

        private CycleReference[,] GenerateReferenceGrid(WorkoutPlanExercise exercise)
        {
            CycleReference[,] references = new CycleReference[TotalWeeks, TotalDays];

            for (int w = 0; w < TotalWeeks; w++)
            {
                for (int d = 0; d < TotalDays; d++)
                {
                    WorkoutRoutineExercise rex = Plan.Routine
                        .RoutineExercises
                        .FirstOrDefault(x => x.WeekId == w + 1 && x.DayId == d + 1 && x.ExerciseId == exercise.ExerciseId);

                    references[w, d] = new CycleReference(exercise, rex);
                }
            }

            return references;
        }

        #endregion

        #region Properties

        public WorkoutPlan Plan { get; private set; }

        public WorkoutPlanCycle Cycle { get; private set; }

        private int TotalWeeks { get { return Plan.Routine.Weeks; } }

        private int TotalDays { get { return Plan.Routine.Days; } }

        #endregion
    }
}