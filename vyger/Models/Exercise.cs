using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Augment;
using YamlDotNet.Serialization;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class Exercise
    {
        #region Constructors

        public Exercise() { }

        public Exercise(Exercise exercise)
        {
            Id = exercise.Id;
            Name = exercise.Name;
            GroupId = exercise.GroupId;
            CategoryId = exercise.CategoryId;
        }

        #endregion

        #region ToString/DebuggerDisplay

        public override string ToString()
        {
            return DebuggerDisplay;
        }

        ///	<summary>
        ///	DebuggerDisplay for this object
        ///	</summary>
        private string DebuggerDisplay
        {
            get
            {
                string id = $"[{Id}]";

                string nm = $"[{Name}]";

                return "{0}, id={1}, nm={2}".FormatArgs(nameof(Exercise), id, nm);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(Exercise other)
        {
            Name = other.Name;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	GROUP(2)-CATEGORY(1)-EXERCISENAME(3)
        ///	</summary>
        [Required]
        [DisplayName("ID")]
        [MinLength(8), MaxLength(8)]
        public string Id
        {
            get { return _id; }
            set { _id = value.ToUpper(); }
        }
        private string _id;

        ///	<summary>
        ///	
        ///	</summary>
		[Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        ///	<summary>
        ///	
        ///	</summary>
		[Required]
        [DisplayName("Category Id")]
        public string CategoryId
        {
            get { return Category == null ? _categoryId : Category.Id; }
            set { _categoryId = value; }
        }
        private string _categoryId;

        ///	<summary>
        ///	
        ///	</summary>
		[Required]
        [DisplayName("Group Id")]
        public string GroupId
        {
            get { return Group == null ? _groupId : Group.Id; }
            set { _groupId = value; }
        }
        private string _groupId;

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	
        ///	</summary>
        [YamlIgnore]
        public ExerciseGroup Group { get; set; }

        ///	<summary>
        ///	
        ///	</summary>
        [YamlIgnore]
        public ExerciseCategory Category { get; set; }

        #endregion
    }
}
