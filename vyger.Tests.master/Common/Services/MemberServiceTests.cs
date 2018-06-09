using System.Data.Entity;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;
using Moq.Language.Flow;

namespace vyger.Tests.Common.Services
{
    [TestClass]
    public class MemberServiceTests : BaseServiceTests<MemberService>
    {
        #region Helpers/Test Initializers

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        #endregion

        #region Tests - Get

        [TestMethod]
        public void MemberService_GetMemberById_Should_CallIntoContext()
        {
            //	arrange
            var request = SecurityAccess.None;

            var members = Design.Many<Member>().Build();

            var member = members.Last();

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.Members)
                .Returns(members);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.None, member));

            //	act
            var actual = SubjectUnderTest.GetMember(member.MemberId, request);

            //	assert
            actual.Should().BeSameAs(member);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void MemberService_GetMemberByEmail_Should_CallIntoContext()
        {
            //	arrange
            var request = SecurityAccess.None;

            var members = Design.Many<Member>().Build();

            var member = members.Last();

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.Members)
                .Returns(members);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.None, member));

            //	act
            var actual = SubjectUnderTest.GetMember(member.MemberEmail, request);

            //	assert
            actual.Should().BeSameAs(member);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Save

        [TestMethod]
        public void MemberService_SaveChange_Should_CallContext()
        {
            //	arrange
            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.SaveChanges())
                .Returns(1);

            //	act
            var actual = SubjectUnderTest.SaveChanges();

            //	assert
            actual.Should().Be(1);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Authenticate

        [TestMethod]
        public void MemberService_Authenicate_Should_FindExistingMember()
        {
            //	arrange
            var token = "x";

            var members = Design.Many<Member>().Build();

            var member = members.Last();

            Moxy.GetMock<IAuthenticationService>()
                .Setup(x => x.VerifyGoogleAuthentication(token))
                .Returns(new AuthenticationToken() { Email = member.MemberEmail });

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.SaveChanges())
                .Returns(1);

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.Members)
                .Returns(members);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.Authenticate, member));

            //	act
            var actual = SubjectUnderTest.AuthenticateLogin(token);

            //	assert
            actual.Should().BeSameAs(member);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void MemberService_Authenicate_Should_CreateMember()
        {
            //	arrange
            var token = "x";

            var members = Design.Many<Member>().Build();

            var member = Design.One<Member>().Build();

            var dbset = Moxy.GetMock<IDbSet<Member>>();

            dbset
                .As<IQueryable<Member>>()
                .Returns(members);

            dbset
                .Setup(x => x.Add(Any.Member))
                .Returns(member);

            Moxy.GetMock<IAuthenticationService>()
                .Setup(x => x.VerifyGoogleAuthentication(token))
                .Returns(new AuthenticationToken() { Email = "new-email" });

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.SaveChanges())
                .Returns(1);

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.Members)
                .Returns(dbset.Object);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.Authenticate, null as Member));

            //	act
            var actual = SubjectUnderTest.AuthenticateLogin(token);

            //	assert
            actual.Should().BeSameAs(member);

            Moxy.VerifyAll();
        }

        #endregion
    }
}
