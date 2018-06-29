using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [XmlRoot("exercise-categories")]
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

        public void AddRange(IEnumerable<ExerciseCategory> categories)
        {
            if (categories != null)
            {
                foreach (ExerciseCategory category in categories)
                {
                    Add(category);
                }
            }
        }

        #endregion
    }
}