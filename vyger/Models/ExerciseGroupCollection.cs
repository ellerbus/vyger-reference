using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [XmlRoot("exercise-groups")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ExerciseGroupCollection : SingleKeyCollection<ExerciseGroup, string>
    {
        #region Constructors

        public ExerciseGroupCollection()
        {
        }

        public ExerciseGroupCollection(IEnumerable<ExerciseGroup> groups) : this()
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

        protected override string GetPrimaryKey(ExerciseGroup item)
        {
            return item.Id;
        }

        public void AddRange(IEnumerable<ExerciseGroup> groups)
        {
            foreach (ExerciseGroup group in groups)
            {
                Add(group);
            }
        }

        #endregion
    }
}