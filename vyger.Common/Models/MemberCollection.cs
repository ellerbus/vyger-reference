using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Augment;

namespace vyger.Common.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MemberCollection : MultiKeyCollection<Member, int, string>
    {
        #region Constructor

        public MemberCollection()
        {

        }

        public MemberCollection(IEnumerable<Member> members) : this()
        {
            foreach (Member member in members)
            {
                Add(member);
            }
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

        protected override int GetPrimaryKey(Member item)
        {
            return item.MemberId;
        }

        protected override string GetUniqueKey(Member item)
        {
            return item.MemberEmail;
        }

        #endregion
    }
}
