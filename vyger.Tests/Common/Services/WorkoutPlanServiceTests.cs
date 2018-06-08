using System.Data.Entity;
using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;

namespace vyger.Tests.Common.Services
{
    [TestClass]
    public class WorkoutPlanServiceTests : BaseServiceTests<WorkoutPlanService>
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
        public void WorkoutPlanService_GetWorkoutPlans_Should_CallIntoContext()
        {
            //	arrange
            var plans = Design.Many<WorkoutPlan>()
                .All()
                .With(x => x.OwnerId = -1)
                .Build();

            plans.Last().OwnerId = 99;

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.WorkoutPlans)
                .Returns(plans);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(99);

            //	act
            var actual = SubjectUnderTest.GetWorkoutPlans();

            //	assert
            actual.Count.Should().Be(plans.Count);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void WorkoutPlanService_GetWorkoutPlans_Should_CallIntoContext_AndFilter()
        {
            //	arrange
            var plans = Design.Many<WorkoutPlan>()
                .All()
                .With(x => x.OwnerId = -1)
                .Build();

            plans.Last().OwnerId = 99;

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.WorkoutPlans)
                .Returns(plans);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(99);

            //	act
            var actual = SubjectUnderTest.GetWorkoutPlans();

            //	assert
            actual.Count.Should().Be(5);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void WorkoutPlanService_GetWorkoutPlanByPrimaryKey_Should_CallIntoContext()
        {
            //	arrange
            var access = SecurityAccess.Update;

            var plans = Design.Many<WorkoutPlan>().Build();

            var plan = plans.Last();

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.WorkoutPlans)
                .Returns(plans);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(access, plan));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(plan.OwnerId);

            //	act
            var actual = SubjectUnderTest.GetWorkoutPlan(plan.PlanId, access);

            //	assert
            actual.Should().BeSameAs(plan);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Add

        [TestMethod]
        public void WorkoutPlanService_AddWorkoutPlan_Should_CallContext()
        {
            //	arrange
            var plan = Design.One<WorkoutPlan>().Build();

            var dbset = Moxy.GetMock<IDbSet<WorkoutPlan>>();

            dbset
                .Setup(x => x.Add(plan))
                .Returns(plan);

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.WorkoutPlans)
                .Returns(dbset.Object);

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.SaveChanges())
                .Returns(1);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(99);

            //	act
            var actual = SubjectUnderTest.AddWorkoutPlan(plan);

            //	assert
            actual.Should().BeSameAs(plan);

            plan.OwnerId.Should().Be(99);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Save

        [TestMethod]
        public void WorkoutPlanService_SaveChange_Should_CallContext()
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
    }
}
