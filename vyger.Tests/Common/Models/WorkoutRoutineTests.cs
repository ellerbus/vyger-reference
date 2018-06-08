using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class WorkoutRoutineTests
    {
        #region Methods

        [TestMethod]
        public void WorkoutRoutine_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<WorkoutRoutine>().Build();
            var b = Design.One<WorkoutRoutine>().Build();

            a.OverlayWith(b);

            a.RoutineName.Should().Be(b.RoutineName);
            a.Weeks.Should().Be(b.Weeks);
            a.Days.Should().Be(b.Days);
            a.StatusEnum.Should().Be(b.StatusEnum);

            a.RoutineId.Should().NotBe(b.RoutineId);
            a.OwnerId.Should().NotBe(b.OwnerId);
            a.CreatedAt.Should().NotBe(b.CreatedAt);
            a.UpdatedAt.Should().NotBe(b.UpdatedAt.GetValueOrDefault());
        }

        #endregion

        #region Property Tests

        //[TestMethod]
        //public void WorkoutRoutine_RoutineId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var routine = new WorkoutRoutine() { RoutineId = 1 };
        //
        //	routine.RoutineId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutine_OwnerId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var routine = new WorkoutRoutine() { OwnerId = 1 };
        //
        //	routine.OwnerId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutine_RoutineName_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var routine = new WorkoutRoutine() { RoutineName = "aa" };
        //
        //	routine.RoutineName.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutine_Weeks_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var routine = new WorkoutRoutine() { Weeks = 1 };
        //
        //	routine.Weeks.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutine_Days_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var routine = new WorkoutRoutine() { Days = 1 };
        //
        //	routine.Days.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutine_StatusEnum_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var routine = new WorkoutRoutine() { StatusEnum = "aa" };
        //
        //	routine.StatusEnum.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutine_CreatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var routine = new WorkoutRoutine() { CreatedAt = DateTime.Now };
        //
        //	routine.CreatedAt.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutine_UpdatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var routine = new WorkoutRoutine() { UpdatedAt = DateTime.Now };
        //
        //	routine.UpdatedAt.Should().Be(expected);
        //}

        #endregion
    }
}
