using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;

namespace vyger.Tests.Common
{
    [TestClass]
    public class SecurityActor_WorkoutRoutineTests : BaseSecurityActor
    {
        [TestMethod]
        public void SecurityActor_WorkoutRoutine_View_Should_AllowOnSystem()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var routine = Design.One<WorkoutRoutine>()
                .With(x => x.OwnerId = -1)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.View, routine);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_WorkoutRoutine_View_Should_AllowOwner()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var routine = Design.One<WorkoutRoutine>()
                .With(x => x.OwnerId = SubjectUnderTest.MemberId)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.View, routine);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_WorkoutRoutine_Create_Should_AllowAnyone()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var routine = Design.One<WorkoutRoutine>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Create, routine);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_WorkoutRoutine_Update_Should_AllowAdmin_OnSystem()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = true;

            var routine = Design.One<WorkoutRoutine>()
                .With(x => x.OwnerId = -1)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, routine);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_WorkoutRoutine_Update_Should_AllowOwner()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var routine = Design.One<WorkoutRoutine>()
                .With(x => x.OwnerId = SubjectUnderTest.MemberId)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, routine);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_WorkoutRoutine_Update_ShouldNot_AllowAnyone()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var routine = Design.One<WorkoutRoutine>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, routine);

            //  assert
            actual.Should().BeFalse();
        }
    }
}
