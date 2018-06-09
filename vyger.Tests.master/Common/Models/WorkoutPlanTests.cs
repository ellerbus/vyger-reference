using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class WorkoutPlanTests
    {
        #region Methods

        [TestMethod]
        public void WorkoutPlan_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<WorkoutPlan>().Build();
            var b = Design.One<WorkoutPlan>().Build();

            a.OverlayWith(b);

            a.OwnerId.Should().Be(b.OwnerId);
            a.RoutineId.Should().Be(b.RoutineId);
            a.StatusEnum.Should().Be(b.StatusEnum);

            a.PlanId.Should().NotBe(b.PlanId);
            a.CreatedAt.Should().NotBe(b.CreatedAt);
            a.UpdatedAt.Should().NotBe(b.UpdatedAt.GetValueOrDefault());
        }

        #endregion

        #region Property Tests

        //[TestMethod]
        //public void WorkoutPlan_PlanId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var plan = new WorkoutPlan() { PlanId = 1 };
        //
        //	plan.PlanId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlan_OwnerId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var plan = new WorkoutPlan() { OwnerId = 1 };
        //
        //	plan.OwnerId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlan_RoutineId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var plan = new WorkoutPlan() { RoutineId = 1 };
        //
        //	plan.RoutineId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlan_StatusEnum_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var plan = new WorkoutPlan() { StatusEnum = "aa" };
        //
        //	plan.StatusEnum.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlan_CreatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var plan = new WorkoutPlan() { CreatedAt = DateTime.Now };
        //
        //	plan.CreatedAt.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlan_UpdatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var plan = new WorkoutPlan() { UpdatedAt = DateTime.Now };
        //
        //	plan.UpdatedAt.Should().Be(expected);
        //}

        #endregion
    }
}
