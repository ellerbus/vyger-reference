using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;

namespace vyger.Tests.Common
{
    [TestClass]
    public class SecurityActor_WorkoutPlanTests : BaseSecurityActor
    {
        [TestMethod]
        public void SecurityActor_WorkoutPlan_View_Should_AllowOnSystem()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var plan = Design.One<WorkoutPlan>()
                .With(x => x.OwnerId = -1)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.View, plan);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_WorkoutPlan_View_Should_AllowOwner()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var plan = Design.One<WorkoutPlan>()
                .With(x => x.OwnerId = SubjectUnderTest.MemberId)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.View, plan);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_WorkoutPlan_Create_Should_AllowAnyone()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var plan = Design.One<WorkoutPlan>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Create, plan);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_WorkoutPlan_Update_Should_AllowAdmin_OnSystem()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = true;

            var plan = Design.One<WorkoutPlan>()
                .With(x => x.OwnerId = -1)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, plan);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_WorkoutPlan_Update_Should_AllowOwner()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var plan = Design.One<WorkoutPlan>()
                .With(x => x.OwnerId = SubjectUnderTest.MemberId)
                .Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, plan);

            //  assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void SecurityActor_WorkoutPlan_Update_ShouldNot_AllowAnyone()
        {
            //  arrange
            SubjectUnderTest.Member.IsAdministrator = false;

            var plan = Design.One<WorkoutPlan>().Build();

            //  act
            var actual = SubjectUnderTest.Can(SecurityAccess.Update, plan);

            //  assert
            actual.Should().BeFalse();
        }
    }
}
