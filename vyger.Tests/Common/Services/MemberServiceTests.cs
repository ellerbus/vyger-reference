using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Repositories;
using vyger.Common.Services;

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

            var member = Design.One<Member>().Build();

            Moxy.GetMock<IMemberRepository>()
                .Setup(x => x.SelectOneById(member.MemberId))
                .Returns(member);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(request, member));

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

            Moxy.GetMock<IMemberRepository>()
                .Setup(x => x.SelectOneByEmail(member.MemberEmail))
                .Returns(member);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(request, member));

            //	act
            var actual = SubjectUnderTest.GetMember(member.MemberEmail, request);

            //	assert
            actual.Should().BeSameAs(member);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Authenticate

        [TestMethod]
        public void MemberService_Authenicate_Should_FindExistingMember()
        {
            //	arrange
            var token = "x";

            var member = Design.One<Member>().Build();

            Moxy.GetMock<IAuthenticationService>()
                .Setup(x => x.VerifyGoogleAuthentication(token))
                .Returns(new AuthenticationToken() { Email = member.MemberEmail });

            Moxy.GetMock<IMemberRepository>()
                .Setup(x => x.SelectOneByEmail(member.MemberEmail))
                .Returns(member);

            Moxy.GetMock<IMemberRepository>()
                .Setup(x => x.Save(Any.Member));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.Authenticate, member));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.EnsureAudit(Any.Member));

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

            Moxy.GetMock<IAuthenticationService>()
                .Setup(x => x.VerifyGoogleAuthentication(token))
                .Returns(new AuthenticationToken() { Email = member.MemberEmail });

            Moxy.GetMock<IMemberRepository>()
                .Setup(x => x.SelectOneByEmail(member.MemberEmail))
                .Returns(null as Member);

            Moxy.GetMock<IMemberRepository>()
                .Setup(x => x.Save(Any.Member));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.EnsureAudit(Any.Member));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.Authenticate, null as Member));

            //	act
            var actual = SubjectUnderTest.AuthenticateLogin(token);

            //	assert

            Moxy.VerifyAll();
        }

        #endregion
    }
}
