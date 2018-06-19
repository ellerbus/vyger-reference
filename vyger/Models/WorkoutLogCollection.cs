using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutLogCollection : Collection<WorkoutLog>
    {
        #region Members

        private ExerciseCollection _exercises;

        #endregion

        #region Constructors

        public WorkoutLogCollection(ExerciseCollection exercises)
        {
            _exercises = exercises;
        }

        public WorkoutLogCollection(ExerciseCollection exercises, IEnumerable<WorkoutLog> logs)
            : this(exercises)
        {
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

        protected override void InsertItem(int index, WorkoutLog item)
        {
            base.InsertItem(index, item);

            UpdateReferences(item);
        }

        protected override void SetItem(int index, WorkoutLog item)
        {
            base.SetItem(index, item);

            UpdateReferences(item);
        }

        private void UpdateReferences(WorkoutLog item)
        {
            item.Exercise = _exercises.GetByPrimaryKey(item.ExerciseId);
        }

        public void AddRange(IEnumerable<WorkoutLog> routines)
        {
            foreach (WorkoutLog routine in routines)
            {
                Add(routine);
            }
        }

        /// <summary>
        /// Gets a single WorkoutLog based on the given primary key
        /// </summary>
        public IEnumerable<WorkoutLog> GetWorkoutLogs(DateTime logDate)
        {
            return this
                .Where(x => x.LogDate == logDate)
                .OrderBy(x => x.SequenceNumber)
                .ThenBy(x => x.Exercise.Name);
        }

        /// <summary>
        /// Gets a single WorkoutLog based on the given primary key
        /// </summary>
        public IList<WorkoutLog> GetWorkoutLogs(string planId, int cycleId)
        {
            return this
                .Where(x => x.PlanId == planId && x.CycleId == cycleId)
                .OrderBy(x => x.SequenceNumber)
                .ThenBy(x => x.Exercise.Name)
                .ToList();
        }

        public IEnumerable<WorkoutLog> GetMostRecent()
        {
            HashSet<string> exercises = new HashSet<string>();

            IEnumerable<WorkoutLog> logs = this.OrderByDescending(x => x.LogDate);

            //  workout backwards
            foreach (WorkoutLog log in logs)
            {
                if (!exercises.Contains(log.ExerciseId))
                {
                    yield return log;

                    exercises.Add(log.ExerciseId);
                }
            }
        }

        #endregion
    }
}