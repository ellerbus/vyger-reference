using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;

namespace vyger.Tests.Common
{
    [TestClass]
    public class SecurityActor_ExerciseGroupTests : BaseSecurityActor
    {
        [TestMethod]
        public void SecurityActor_ExerciseGroup_View_Should_AllowAnyone()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var group = Design.One<ExerciseGroup>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.View, group);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_ExerciseGroup_Create_Should_AllowAdmin()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = true;

            var group = Design.One<ExerciseGroup>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Create, group);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_ExerciseGroup_Create_ShouldNot_AllowAnyone()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var group = Design.One<ExerciseGroup>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Create, group);

            //  assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void SecurityActor_ExerciseGroup_Update_Should_AllowAdmin()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = true;

            var group = Design.One<ExerciseGroup>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, group);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_ExerciseGroup_Update_ShouldNot_AllowAnyone()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var group = Design.One<ExerciseGroup>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, group);

            //  assert
            actual.Should().BeFalse();
        }
    }
}
