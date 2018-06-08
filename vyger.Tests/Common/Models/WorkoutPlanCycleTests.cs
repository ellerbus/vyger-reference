using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class WorkoutPlanCycleTests
    {
        #region Methods

        [TestMethod]
        public void WorkoutPlanCycle_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<WorkoutPlanCycle>().Build();
            var b = Design.One<WorkoutPlanCycle>().Build();

            a.OverlayWith(b);

            a.StatusEnum.Should().Be(b.StatusEnum);

            a.PlanId.Should().NotBe(b.PlanId);
            a.CycleId.Should().NotBe(b.CycleId);
            a.CreatedAt.Should().NotBe(b.CreatedAt);
            a.UpdatedAt.Should().NotBe(b.UpdatedAt.GetValueOrDefault());
        }

        #endregion

        #region Property Tests

        //[TestMethod]
        //public void WorkoutPlanCycle_PlanId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var cycle = new WorkoutPlanCycle() { PlanId = 1 };
        //
        //	cycle.PlanId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanCycle_CycleId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var cycle = new WorkoutPlanCycle() { CycleId = 1 };
        //
        //	cycle.CycleId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanCycle_StatusEnum_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var cycle = new WorkoutPlanCycle() { StatusEnum = "aa" };
        //
        //	cycle.StatusEnum.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanCycle_CreatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var cycle = new WorkoutPlanCycle() { CreatedAt = DateTime.Now };
        //
        //	cycle.CreatedAt.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanCycle_UpdatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var cycle = new WorkoutPlanCycle() { UpdatedAt = DateTime.Now };
        //
        //	cycle.UpdatedAt.Should().Be(expected);
        //}

        #endregion
    }
}
