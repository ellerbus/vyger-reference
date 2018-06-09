using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Models;

namespace vyger.Tests.Models
{
    [TestClass]
    public class MemberTests
    {
        #region Methods

        [TestMethod]
        public void Member_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<Member>().Build();
            var b = Design.One<Member>().Build();

            a.OverlayWith(b);

            a.Email.Should().Be(b.Email);

            a.UpdatedAt.Should().NotBe(b.UpdatedAt);
        }

        #endregion

        #region Property Tests

        [TestMethod]
        public void Member_Email_Should_LowerCase()
        {
            var member = new Member() { Email = "AA" };

            member.Email.Should().Be("aa");
        }

        #endregion
    }
}
