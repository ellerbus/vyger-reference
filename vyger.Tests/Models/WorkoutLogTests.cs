using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class WorkoutLogTests
    {
        #region Methods

        [TestMethod]
        public void WorkoutLog_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<WorkoutLog>().Build();
            var b = Design.One<WorkoutLog>().Build();

            a.OverlayWith(b);

            a.Workout.Should().Be(b.Workout);
            a.PlanId.Should().Be(b.PlanId);
            a.CycleId.Should().Be(b.CycleId);
            a.WeekId.Should().Be(b.WeekId);
            a.DayId.Should().Be(b.DayId);
            a.SequenceNumber.Should().Be(b.SequenceNumber);

            a.MemberId.Should().NotBe(b.MemberId);
            a.LogDate.Should().NotBe(b.LogDate);
            a.ExerciseId.Should().NotBe(b.ExerciseId);
            a.CreatedAt.Should().NotBe(b.CreatedAt);
            a.UpdatedAt.Should().NotBe(b.UpdatedAt.GetValueOrDefault());
        }

        #endregion

        #region Property Tests

        //[TestMethod]
        //public void WorkoutLog_MemberId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutLog() { MemberId = 1 };
        //
        //	log.MemberId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutLog_LogDate_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var log = new WorkoutLog() { LogDate = DateTime.Now };
        //
        //	log.LogDate.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutLog_ExerciseId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutLog() { ExerciseId = 1 };
        //
        //	log.ExerciseId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutLog_Workout_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var log = new WorkoutLog() { Workout = "aa" };
        //
        //	log.Workout.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutLog_PlanId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutLog() { PlanId = 1 };
        //
        //	log.PlanId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutLog_CycleId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutLog() { CycleId = 1 };
        //
        //	log.CycleId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutLog_WeekId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutLog() { WeekId = 1 };
        //
        //	log.WeekId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutLog_DayId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutLog() { DayId = 1 };
        //
        //	log.DayId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutLog_SequenceNumber_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var log = new WorkoutLog() { SequenceNumber = 1 };
        //
        //	log.SequenceNumber.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutLog_CreatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var log = new WorkoutLog() { CreatedAt = DateTime.Now };
        //
        //	log.CreatedAt.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutLog_UpdatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var log = new WorkoutLog() { UpdatedAt = DateTime.Now };
        //
        //	log.UpdatedAt.Should().Be(expected);
        //}

        #endregion
    }
}
