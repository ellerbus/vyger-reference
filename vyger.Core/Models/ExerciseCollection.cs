using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using Augment;

namespace vyger.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [XmlRoot("exercises")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ExerciseCollection : SingleKeyCollection<Exercise, string>
    {
        #region Constructors

        public ExerciseCollection()
        {
        }

        public ExerciseCollection(
            ExerciseGroupCollection groups,
            ExerciseCategoryCollection categories,
            IEnumerable<Exercise> exercises)
            : this()
        {
            Groups = groups;
            Categories = categories;

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

        public IEnumerable<Exercise> Filter(string groupId, string categoryId)
        {
            return this
                .Where(x => groupId.IsNullOrEmpty() || x.GroupId.IsSameAs(groupId))
                .Where(x => categoryId.IsNullOrEmpty() || x.CategoryId.IsSameAs(categoryId));
        }

        protected override string GetPrimaryKey(Exercise item)
        {
            return item.Id;
        }

        protected override void InsertItem(int index, Exercise item)
        {
            base.InsertItem(index, item);

            UpdateReferences(item);
        }

        protected override void SetItem(int index, Exercise item)
        {
            base.SetItem(index, item);

            UpdateReferences(item);
        }

        private void UpdateReferences(Exercise item)
        {
            if (Groups != null)
            {
                item.Group = Groups.GetByPrimaryKey(item.GroupId);
            }

            if (Categories != null)
            {
                item.Category = Categories.GetByPrimaryKey(item.CategoryId);
            }
        }

        public void AddRange(IEnumerable<Exercise> exercises)
        {
            if (exercises != null)
            {
                foreach (Exercise exercise in exercises)
                {
                    Add(exercise);
                }
            }
        }

        #endregion

        #region Foreign Keys

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore]
        public ExerciseCategoryCollection Categories { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore]
        public ExerciseGroupCollection Groups { get; set; }

        #endregion
    }
}