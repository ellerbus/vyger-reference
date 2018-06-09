using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Augment;
using vyger.Common;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Member
    {
        #region Constructors

        public Member() { }

        public Member(string email)
        {
            Email = email;
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
                string id = $"[{Email}]";

                return "{0}, id={1}".FormatArgs(nameof(Member), id);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(Member other)
        {
            Email = other.Email;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	
        ///	</summary>
		[Required]
        [DisplayName("Email")]
        public string Email
        {
            get { return _email; }
            set { _email = value.AssertNotNull().ToLower(); }
        }
        private string _email;

        ///	<summary>
        ///	
        ///	</summary>
        public DateTime UpdatedAt
        {
            get { return _updatedAt.EnsureUtc(); }
            set { _updatedAt = value.EnsureUtc(); }
        }
        private DateTime _updatedAt;

        #endregion
    }
}
