using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class WorkoutPlanLogTests
    {
        #region Methods

        [TestMethod]
        public void WorkoutPlanLog_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<WorkoutPlanLog>().Build();
            var b = Design.One<WorkoutPlanLog>().Build();

            a.OverlayWith(b);

            a.SequenceNumber.Should().Be(b.SequenceNumber);
            a.WorkoutPlan.Should().Be(b.WorkoutPlan);
            a.StatusEnum.Should().Be(b.StatusEnum);

            a.PlanId.Should().NotBe(b.PlanId);
            a.CycleId.Should().NotBe(b.CycleId);
            a.WeekId.Should().NotBe(b.WeekId);
            a.DayId.Should().NotBe(b.DayId);
            a.ExerciseId.Should().NotBe(b.ExerciseId);
            a.CreatedAt.Should().NotBe(b.CreatedAt);
            a.UpdatedAt.Should().NotBe(b.UpdatedAt.GetValueOrDefault());
        }

        #endregion

        #region Property Tests

        //[TestMethod]
        //public void WorkoutPlanLog_PlanId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutPlanLog() { PlanId = 1 };
        //
        //	log.PlanId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanLog_CycleId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutPlanLog() { CycleId = 1 };
        //
        //	log.CycleId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanLog_WeekId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutPlanLog() { WeekId = 1 };
        //
        //	log.WeekId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanLog_DayId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutPlanLog() { DayId = 1 };
        //
        //	log.DayId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanLog_ExerciseId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutPlanLog() { ExerciseId = 1 };
        //
        //	log.ExerciseId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanLog_SequenceNumber_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutPlanLog() { SequenceNumber = 1 };
        //
        //	log.SequenceNumber.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanLog_WorkoutPlan_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var log = new WorkoutPlanLog() { WorkoutPlan = "aa" };
        //
        //	log.WorkoutPlan.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanLog_StatusEnum_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var log = new WorkoutPlanLog() { StatusEnum = "aa" };
        //
        //	log.StatusEnum.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanLog_CreatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var log = new WorkoutPlanLog() { CreatedAt = DateTime.Now };
        //
        //	log.CreatedAt.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutPlanLog_UpdatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var log = new WorkoutPlanLog() { UpdatedAt = DateTime.Now };
        //
        //	log.UpdatedAt.Should().Be(expected);
        //}

        #endregion
    }
}
