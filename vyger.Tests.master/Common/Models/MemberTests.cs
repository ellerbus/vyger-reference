using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
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

            a.MemberEmail.Should().Be(b.MemberEmail);
            a.IsAdministrator.Should().Be(b.IsAdministrator);
            a.StatusEnum.Should().Be(b.StatusEnum);

            a.MemberId.Should().NotBe(b.MemberId);
            a.CreatedAt.Should().NotBe(b.CreatedAt);
            a.UpdatedAt.Should().NotBe(b.UpdatedAt.GetValueOrDefault());
        }

        #endregion

        #region Property Tests

        [TestMethod]
        public void Member_MemberEmail_Should_LowerCase()
        {
            var member = new Member() { MemberEmail = "AA" };

            member.MemberEmail.Should().Be("aa");
        }

        [TestMethod]
        public void Member_StatusEnum_Should_DefaultNone()
        {
            var member = new Member();

            member.Status.Should().Be(StatusTypes.None);
        }

        #endregion
    }
}
