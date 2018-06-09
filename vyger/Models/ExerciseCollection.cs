using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Augment;
namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ExerciseCollection : SingleKeyCollection<Exercise, string>
    {
        #region Constructors

        public ExerciseCollection()
        {
        }

        public ExerciseCollection(IEnumerable<Exercise> exercises) : this()
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

        protected override string GetPrimaryKey(Exercise item)
        {
            return item.Id;
        }

        public void AddRange(IEnumerable<Exercise> exercises)
        {
            foreach (Exercise exercise in exercises)
            {
                Add(exercise);
            }
        }

        public IEnumerable<Exercise> NotIncluding(IEnumerable<WorkoutRoutineExercise> routineExercises)
        {
            HashSet<string> ids = new HashSet<string>(routineExercises.Select(x => x.ExerciseId));

            foreach (Exercise ex in this)
            {
                if (!ids.Contains(ex.Id))
                {
                    yield return ex;
                }
            }
        }

        #endregion

        #region Foreign Key Properties

        #endregion
    }
}
