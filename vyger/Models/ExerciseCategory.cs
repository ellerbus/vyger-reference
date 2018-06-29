using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Serialization;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [XmlRoot("exercise-category")]
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
        [Required]
        [DisplayName("ID")]
        [MinLength(1), MaxLength(1)]
        [XmlAttribute("id")]
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
        [XmlAttribute("name")]
        public string Name { get; set; }

        #endregion
    }
}