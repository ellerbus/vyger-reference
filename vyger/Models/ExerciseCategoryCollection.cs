using System.Collections.Generic;
using System.Diagnostics;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ExerciseCategoryCollection : SingleKeyCollection<ExerciseCategory, string>
    {
        #region Constructors

        public ExerciseCategoryCollection()
        {
        }

        public ExerciseCategoryCollection(IEnumerable<ExerciseCategory> groups) : this()
        {
            AddRange(groups);
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

        protected override string GetPrimaryKey(ExerciseCategory item)
        {
            return item.Id;
        }

        public void AddRange(IEnumerable<ExerciseCategory> groups)
        {
            foreach (ExerciseCategory group in groups)
            {
                Add(group);
            }
        }

        #endregion
    }
}
