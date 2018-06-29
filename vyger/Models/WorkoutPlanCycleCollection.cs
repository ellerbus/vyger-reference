using System.Collections.Generic;
using System.Diagnostics;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutPlanCycleCollection : SingleKeyCollection<WorkoutPlanCycle, int>
    {
        #region Constructors

        public WorkoutPlanCycleCollection(WorkoutPlan plan)
        {
            Plan = plan;
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

        protected override int GetPrimaryKey(WorkoutPlanCycle item)
        {
            return item.CycleId;
        }

        protected override void InsertItem(int index, WorkoutPlanCycle item)
        {
            base.InsertItem(index, item);

            UpdateReferences(item);
        }

        protected override void SetItem(int index, WorkoutPlanCycle item)
        {
            base.SetItem(index, item);

            UpdateReferences(item);
        }

        private void UpdateReferences(WorkoutPlanCycle item)
        {
            item.Plan = Plan;

            //if (item.Routine != null && item.Routine.AllExercises != null && item.Exercise == null)
            //{
            //    //  only after the deserializer has finished it's setup
            //    //  to we have the proper handles to the routine and exercises
            //    item.Exercise = Routine.AllExercises.GetByPrimaryKey(item.ExerciseId);
            //}
        }

        public void AddRange(IEnumerable<WorkoutPlanCycle> cycles)
        {
            foreach (WorkoutPlanCycle cycle in cycles)
            {
                Add(cycle);
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

        public WorkoutPlan Plan { get; private set; }

        #endregion
    }
}