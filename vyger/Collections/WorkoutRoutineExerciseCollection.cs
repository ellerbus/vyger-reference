using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Augment;
using EnsureThat;
using vyger.Common.Models;

namespace vyger.Common.Collections
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutRoutineExerciseCollection : Collection<WorkoutRoutineExercise>
    {
        #region Constructors

        public WorkoutRoutineExerciseCollection(WorkoutRoutine routine)
        {
            Routine = routine;
        }

        public WorkoutRoutineExerciseCollection(WorkoutRoutine routine, IEnumerable<WorkoutRoutineExercise> exercises)
            : this(routine)
        {
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

        protected override void InsertItem(int index, WorkoutRoutineExercise item)
        {
            base.InsertItem(index, item);

            UpdateReferences(item);
        }

        protected override void SetItem(int index, WorkoutRoutineExercise item)
        {
            base.SetItem(index, item);

            UpdateReferences(item);
        }

        private void UpdateReferences(WorkoutRoutineExercise item)
        {
            item.Routine = Routine;

            if (item.Routine != null && item.Exercise == null)
            {
                item.Exercise = Routine.AllExercises.GetByPrimaryKey(item.ExerciseId);
            }

            if (WeekId > 0)
            {
                Ensure.That(item.WeekId, nameof(WeekId)).Is(WeekId);
            }

            if (DayId > 0)
            {
                Ensure.That(item.DayId, nameof(DayId)).Is(DayId);
            }
        }

        public void AddRange(IEnumerable<WorkoutRoutineExercise> exercises)
        {
            foreach (WorkoutRoutineExercise exercise in exercises)
            {
                Add(exercise);
            }
        }

        #endregion

        #region Foreign Key Properties

        public WorkoutRoutine Routine { get; private set; }

        /// <summary>
        /// Only set if this collection is a subset (or a single week basically)
        /// &gt 1 means there's a week assigned
        /// </summary>
        public int WeekId { get; set; }

        /// <summary>
        /// Only set if this collection is a subset (or a single day basically)
        /// &gt 1 means there's a day assigned
        /// </summary>
        public int DayId { get; set; }

        /// <summary>
        /// Only set if this collection is a subset (or a single exercise basically)
        /// &gt 1 means there's a exercise assigned
        /// </summary>
        public int ExerciseId { get; set; }

        #endregion
    }
}
