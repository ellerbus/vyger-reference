using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [XmlRoot("workout-routine-exercises")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutRoutineExerciseCollection : Collection<WorkoutRoutineExercise>
    {
        #region Constructors

        public WorkoutRoutineExerciseCollection(WorkoutRoutine routine)
        {
            Routine = routine;
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

            if (item.Routine != null && item.Routine.AllExercises != null && item.Exercise == null)
            {
                //  only after the deserializer has finished it's setup
                //  to we have the proper handles to the routine and exercises
                item.Exercise = Routine.AllExercises.GetByPrimaryKey(item.ExerciseId);
            }

            //if (WeekId > 0)
            //{
            //    Ensure.That(item.WeekId, nameof(WeekId)).Is(WeekId);
            //}

            //if (DayId > 0)
            //{
            //    Ensure.That(item.DayId, nameof(DayId)).Is(DayId);
            //}
        }

        public void AddRange(IEnumerable<WorkoutRoutineExercise> exercises)
        {
            foreach (WorkoutRoutineExercise exercise in exercises)
            {
                Add(exercise);
            }
        }

        public IEnumerable<WorkoutRoutineExercise> Find(int weekId, int dayId, string exerciseId)
        {
            return this
                .Where(x => weekId == 0 || x.WeekId == weekId)
                .Where(x => dayId == 0 || x.DayId == dayId)
                .Where(x => exerciseId.IsNullOrEmpty() || x.ExerciseId.IsSameAs(exerciseId))
                .OrderBy(x => x.WeekId)
                .ThenBy(x => x.DayId)
                .ThenBy(x => x.SequenceNumber)
                .ThenBy(x => x.Exercise.Name);
        }

        public void Add(int dayId, string exerciseId, string workoutRoutine)
        {
            workoutRoutine = WorkoutRoutineSetCollection.Format(workoutRoutine);

            Exercise exercise = Routine.AllExercises.GetByPrimaryKey(exerciseId);

            List<WorkoutRoutineExercise> routineExercises = new List<WorkoutRoutineExercise>();

            for (int w = 0; w < Routine.Weeks; w++)
            {
                WorkoutRoutineExercise routineExercise = new WorkoutRoutineExercise()
                {
                    Routine = Routine,
                    Exercise = exercise,
                    WeekId = w + 1,
                    DayId = dayId,
                    WorkoutRoutine = workoutRoutine,
                    SequenceNumber = 99
                };

                Add(routineExercise);
            }
        }

        public void DeleteWorkoutRoutineExercise(int dayId, string exerciseId)
        {
            IList<WorkoutRoutineExercise> remove = Find(0, dayId, exerciseId).ToList();

            foreach (WorkoutRoutineExercise ex in remove)
            {
                Remove(ex);
            }
        }

        #endregion

        #region Foreign Key Properties

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore]
        public WorkoutRoutine Routine { get; private set; }

        ///// <summary>
        ///// Only set if this collection is a subset (or a single week basically)
        ///// &gt 1 means there's a week assigned
        ///// </summary>
        //public int WeekId { get; set; }

        ///// <summary>
        ///// Only set if this collection is a subset (or a single day basically)
        ///// &gt 1 means there's a day assigned
        ///// </summary>
        //public int DayId { get; set; }

        ///// <summary>
        ///// Only set if this collection is a subset (or a single exercise basically)
        ///// &gt 1 means there's a exercise assigned
        ///// </summary>
        //public int ExerciseId { get; set; }

        #endregion
    }
}