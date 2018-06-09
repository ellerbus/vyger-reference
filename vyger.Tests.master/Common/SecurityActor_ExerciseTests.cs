using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;

namespace vyger.Tests.Common
{
    [TestClass]
    public class SecurityActor_ExerciseTests : BaseSecurityActor
    {
        [TestMethod]
        public void SecurityActor_Exercise_View_Should_AllowOnSystem()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var exercise = Design.One<Exercise>()
                .With(x => x.OwnerId = -1)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.View, exercise);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_Exercise_View_Should_AllowOwner()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var exercise = Design.One<Exercise>()
                .With(x => x.OwnerId = SubjectUnderTest.MemberId)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.View, exercise);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_Exercise_Create_Should_AllowAnyone()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var exercise = Design.One<Exercise>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Create, exercise);

            //  assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void SecurityActor_Exercise_Update_Should_AllowAdmin_OnSystem()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = true;

            var exercise = Design.One<Exercise>()
                .With(x => x.OwnerId = -1)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, exercise);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_Exercise_Update_Should_AllowOwner()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var exercise = Design.One<Exercise>()
                .With(x => x.OwnerId = SubjectUnderTest.MemberId)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, exercise);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_Exercise_Update_ShouldNot_AllowAnyone()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var exercise = Design.One<Exercise>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, exercise);

            //  assert
            actual.Should().BeFalse();
        }
    }
}
