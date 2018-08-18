using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using Augment;

namespace vyger.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [XmlRoot("workout-logs")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutLogCollection : Collection<WorkoutLog>
    {
        #region Constructors

        public WorkoutLogCollection()
        {
        }

        public WorkoutLogCollection(ExerciseCollection exercises, IEnumerable<WorkoutLog> logs)
            : this()
        {
            Exercises = exercises;

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
            if (Exercises != null)
            {
                item.Exercise = Exercises.GetByPrimaryKey(item.ExerciseId);
            }
        }

        public void AddRange(IEnumerable<WorkoutLog> routines)
        {
            if (routines != null)
            {
                foreach (WorkoutLog routine in routines)
                {
                    Add(routine);
                }
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
        public IEnumerable<WorkoutLog> GetRecentWorkoutLogs(string planId, int cycleId)
        {
            if (cycleId > 1)
            {
                IEnumerable<WorkoutLog> lastCycle = this
                   .Where(x => x.PlanId == planId && x.CycleId == cycleId)
                   .OrderBy(x => x.SequenceNumber)
                   .ThenBy(x => x.Exercise.Name);

                foreach (WorkoutLog item in lastCycle)
                {
                    yield return item;
                }
            }

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

        public void RemoveAll(DateTime logDate)
        {
            IList<WorkoutLog> remove = this.Where(x => x.LogDate == logDate).ToList();

            foreach (WorkoutLog log in remove)
            {
                Remove(log);
            }
        }

        #endregion

        #region Foreign Keys

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore]
        public ExerciseCollection Exercises { get; set; }

        #endregion
    }
}