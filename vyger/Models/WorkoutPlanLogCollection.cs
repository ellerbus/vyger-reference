using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Augment;
using vyger.Common;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutPlanLogCollection : Collection<WorkoutPlanLog>
    {
        #region Constructors

        public WorkoutPlanLogCollection()
        {
        }

        public WorkoutPlanLogCollection(WorkoutPlanCycle cycle, IEnumerable<WorkoutPlanLog> logs)
        {
            Cycle = cycle;

            AddRange(logs);
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

        protected override void InsertItem(int index, WorkoutPlanLog item)
        {
            base.InsertItem(index, item);

            UpdateReferences(item);
        }

        protected override void SetItem(int index, WorkoutPlanLog item)
        {
            base.SetItem(index, item);

            UpdateReferences(item);
        }

        private void UpdateReferences(WorkoutPlanLog item)
        {
            item.Cycle = Cycle;

            if (item.Cycle != null && item.Cycle.PlanExercises != null)
            {
                //  only after the deserializer has finished it's setup
                //  to we have the proper handles to the routine and logs
                item.PlanExercise = item.Cycle.PlanExercises.GetByPrimaryKey(item.ExerciseId);
            }
        }

        public void AddRange(IEnumerable<WorkoutPlanLog> logs)
        {
            foreach (WorkoutPlanLog log in logs)
            {
                Add(log);
            }
        }

        public IEnumerable<WorkoutPlanLog> GetLogsFor(int weekId, int dayId)
        {
            return this
                .Where(x => x.WeekId == weekId && x.DayId == dayId)
                .OrderBy(x => x.SequenceNumber);
        }

        public bool CanCreateLogInputFor(int weekId, int dayId)
        {
            return GetLogsFor(weekId, dayId).Any(x => x.Status != StatusTypes.Complete);
        }

        //public IEnumerable<WorkoutPlanCycle> Find(int weekId, int dayId, string logId)
        //{
        //    return this
        //        .Where(x => weekId == 0 || x.WeekId == weekId)
        //        .Where(x => dayId == 0 || x.DayId == dayId)
        //        .Where(x => logId.IsNullOrEmpty() || x.LogId.IsSameAs(logId))
        //        .OrderBy(x => x.WeekId)
        //        .ThenBy(x => x.DayId)
        //        .ThenBy(x => x.SequenceNumber)
        //        .ThenBy(x => x.Log.Name);
        //}

        //public void Add(int dayId, string logId, string workoutRoutine)
        //{
        //    workoutRoutine = WorkoutRoutineSetCollection.Format(workoutRoutine);

        //    Log log = Routine.AllLogs.GetByPrimaryKey(logId);

        //    List<WorkoutPlanCycle> routineLogs = new List<WorkoutPlanCycle>();

        //    for (int w = 0; w < Routine.Weeks; w++)
        //    {
        //        WorkoutPlanCycle routineLog = new WorkoutPlanCycle()
        //        {
        //            Routine = Routine,
        //            Log = log,
        //            WeekId = w + 1,
        //            DayId = dayId,
        //            WorkoutRoutine = workoutRoutine,
        //            SequenceNumber = 99
        //        };

        //        Add(routineLog);
        //    }
        //}

        //public void DeleteWorkoutRoutineLog(int dayId, string logId)
        //{
        //    IList<WorkoutPlanCycle> remove = Find(0, dayId, logId).ToList();

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