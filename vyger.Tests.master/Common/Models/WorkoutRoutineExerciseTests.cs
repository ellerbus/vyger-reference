using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class WorkoutRoutineExerciseTests
    {
        #region Methods

        [TestMethod]
        public void WorkoutRoutineExercise_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<WorkoutRoutineExercise>().Build();
            var b = Design.One<WorkoutRoutineExercise>().Build();

            a.OverlayWith(b);

            a.SequenceNumber.Should().Be(b.SequenceNumber);
            a.WorkoutRoutine.Should().Be(b.WorkoutRoutine);

            a.RoutineId.Should().NotBe(b.RoutineId);
            a.WeekId.Should().NotBe(b.WeekId);
            a.DayId.Should().NotBe(b.DayId);
            a.ExerciseId.Should().NotBe(b.ExerciseId);
            a.CreatedAt.Should().NotBe(b.CreatedAt);
            a.UpdatedAt.Should().NotBe(b.UpdatedAt.GetValueOrDefault());
        }

        #endregion

        #region Property Tests

        //[TestMethod]
        //public void WorkoutRoutineExercise_RoutineId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var exercise = new WorkoutRoutineExercise() { RoutineId = 1 };
        //
        //	exercise.RoutineId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutineExercise_WeekId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var exercise = new WorkoutRoutineExercise() { WeekId = 1 };
        //
        //	exercise.WeekId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutineExercise_DayId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var exercise = new WorkoutRoutineExercise() { DayId = 1 };
        //
        //	exercise.DayId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutineExercise_ExerciseId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var exercise = new WorkoutRoutineExercise() { ExerciseId = 1 };
        //
        //	exercise.ExerciseId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutineExercise_SequenceNumber_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var exercise = new WorkoutRoutineExercise() { SequenceNumber = 1 };
        //
        //	exercise.SequenceNumber.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutineExercise_WorkoutRoutine_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var exercise = new WorkoutRoutineExercise() { WorkoutRoutine = "aa" };
        //
        //	exercise.WorkoutRoutine.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutineExercise_CreatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var exercise = new WorkoutRoutineExercise() { CreatedAt = DateTime.Now };
        //
        //	exercise.CreatedAt.Should().Be(expected);
        //}

        //[TestMethod]
        //public void WorkoutRoutineExercise_UpdatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var exercise = new WorkoutRoutineExercise() { UpdatedAt = DateTime.Now };
        //
        //	exercise.UpdatedAt.Should().Be(expected);
        //}

        #endregion
    }
}
