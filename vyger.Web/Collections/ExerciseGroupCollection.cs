using System;
using System.Collections.Generic;
using System.Diagnostics;
using Augment;
using vyger.Common.Models;

namespace vyger.Common.Collections
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ExerciseGroupCollection : SingleKeyCollection<ExerciseGroup, int>
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

        protected override int GetPrimaryKey(ExerciseGroup item)
        {
            return item.GroupId;
        }

        public void AddRange(IEnumerable<ExerciseGroup> groups)
        {
            foreach (ExerciseGroup group in groups)
            {
                Add(group);
            }
        }

        #endregion

        #region Foreign Key Properties

        #endregion
    }
}
