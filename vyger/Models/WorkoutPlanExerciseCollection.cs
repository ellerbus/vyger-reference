using System;
using System.Collections.Generic;
using System.Diagnostics;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutPlanExerciseCollection : SingleKeyCollection<WorkoutPlanExercise, string>
    {
        #region Constructors

        public WorkoutPlanExerciseCollection()
        {
        }

        public WorkoutPlanExerciseCollection(WorkoutPlanCycle cycle, IEnumerable<WorkoutPlanExercise> exercises)
        {
            Cycle = cycle;

            AddRange(exercises);
        }

        #endregion

        #region ToString/DebuggerDisplay

        ///	<summary>
        ///	DebuggerDisplay for this object
        ///	</summary>
        private string DebuggerDisplay
        {
            get { return "Count={0}".FormatArgs(Count); }
        }

        #endregion

        #region Methods

        protected override string GetPrimaryKey(WorkoutPlanExercise item)
        {
            return item.ExerciseId;
        }

        protected override void InsertItem(int index, WorkoutPlanExercise item)
        {
            base.InsertItem(index, item);

            UpdateReferences(item);
        }

        protected override void SetItem(int index, WorkoutPlanExercise item)
        {
            base.SetItem(index, item);

            UpdateReferences(item);
        }

        private void UpdateReferences(WorkoutPlanExercise item)
        {
            item.Cycle = Cycle;

            if (item.Cycle != null && item.Cycle.Plan != null && item.Cycle.Plan.Routine != null)
            {
                WorkoutRoutine routine = item.Cycle.Plan.Routine;

                if (routine.AllExercises != null && item.Exercise == null)
                {
                    //  only after the deserializer has finished it's setup
                    //  to we have the proper handles to the routine and exercises
                    item.Exercise = routine.AllExercises.GetByPrimaryKey(item.ExerciseId);
                }
            }
        }

        public void AddRange(IEnumerable<WorkoutPlanExercise> exercises)
        {
            foreach (WorkoutPlanExercise exercise in exercises)
            {
                Add(exercise);
            }
        }

        public void OverlayWith(IEnumerable<WorkoutPlanExercise> planExercises)
        {
            foreach(WorkoutPlanExercise source in planExercises)
            {
                WorkoutPlanExercise target = GetByPrimaryKey(source.ExerciseId);

                target.OverlayWith(source);
            }
        }

        //public IEnumerable<WorkoutPlanCycle> Find(int weekId, int dayId, string exerciseId)
        //{
        //    return this
        //        .Where(x => weekId == 0 || x.WeekId == weekId)
        //        .Where(x => dayId == 0 || x.DayId == dayId)
        //        .Where(x => exerciseId.IsNullOrEmpty() || x.ExerciseId.IsSameAs(exerciseId))
        //        .OrderBy(x => x.WeekId)
        //        .ThenBy(x => x.DayId)
        //        .ThenBy(x => x.SequenceNumber)
        //        .ThenBy(x => x.Exercise.Name);
        //}

        //public void Add(int dayId, string exerciseId, string workoutRoutine)
        //{
        //    workoutRoutine = WorkoutRoutineSetCollection.Format(workoutRoutine);

        //    Exercise exercise = Routine.AllExercises.GetByPrimaryKey(exerciseId);

        //    List<WorkoutPlanCycle> routineExercises = new List<WorkoutPlanCycle>();

        //    for (int w = 0; w < Routine.Weeks; w++)
        //    {
        //        WorkoutPlanCycle routineExercise = new WorkoutPlanCycle()
        //        {
        //            Routine = Routine,
        //            Exercise = exercise,
        //            WeekId = w + 1,
        //            DayId = dayId,
        //            WorkoutRoutine = workoutRoutine,
        //            SequenceNumber = 99
        //        };

        //        Add(routineExercise);
        //    }
        //}

        //public void DeleteWorkoutRoutineExercise(int dayId, string exerciseId)
        //{
        //    IList<WorkoutPlanCycle> remove = Find(0, dayId, exerciseId).ToList();

        //    foreach (WorkoutPlanCycle ex in remove)
        //    {
        //        Remove(ex);
        //    }
        //}

        #endregion

        #region Foreign Key Properties

        public WorkoutPlanCycle Cycle { get; private set; }

        #endregion
    }
}