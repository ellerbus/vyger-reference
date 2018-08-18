using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Serialization;
using Augment;

namespace vyger.Core.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [XmlType("exercise-category")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ExerciseCategory
    {
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

                return "{0}, id={1}, nm={2}".FormatArgs(nameof(ExerciseCategory), id, nm);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(ExerciseCategory other)
        {
            Name = other.Name;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///
        ///	</summary>
        [DisplayName("ID")]
        [XmlAttribute("id")]
        public string Id
        {
            get { return _id; }
            set { _id = value.AssertNotNull().ToUpper(); }
        }

        private string _id;

        ///	<summary>
        ///
        ///	</summary>
        [DisplayName("Name")]
        [XmlAttribute("name")]
        public string Name { get; set; }

        #endregion
    }
}