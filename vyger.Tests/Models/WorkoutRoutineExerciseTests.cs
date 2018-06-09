using System;
using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class WorkoutRoutineExerciseTests
    {
        #region Constructor Tests

        [TestMethod]
        public void WorkoutRoutineExercise_EmptyConstructor_Should_FlagModified()
        {
            var exercise = new WorkoutRoutineExercise()
            {
                RoutineId = 1,
                WeekId = 1,
                DayId = 1,
                ExerciseId = 1,
                SequenceNumber = 1,
                WorkoutRoutine = "aa",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            exercise.IsModified.Should().BeTrue();
        }

        [TestMethod]
        public void WorkoutRoutineExercise_FullConstructor_Should_FlagNotModified()
        {
            var exercise = new WorkoutRoutineExercise(
                routineId: 1,
                weekId: 1,
                dayId: 1,
                exerciseId: 1,
                sequenceNumber: 1,
                workoutRoutine: "aa",
                createdAt: DateTime.Now,
                updatedAt: DateTime.Now
                );

            exercise.IsModified.Should().BeFalse();
        }

        #endregion

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

        [TestMethod]
        public void WorkoutRoutine_RoutineId_Should_UseBase()
        {
            var exercise = new WorkoutRoutineExercise() { RoutineId = 1 };

            exercise.RoutineId.Should().Be(1);
        }

        [TestMethod]
        public void WorkoutRoutineExercise_RoutineId_Should_UseReference()
        {
            var exercise = new WorkoutRoutineExercise()
            {
                RoutineId = 1,
                Routine = Design.One<WorkoutRoutine>().Build()
            };

            exercise.RoutineId.Should().Be(exercise.Routine.RoutineId);
        }

        [TestMethod]
        public void WorkoutRoutine_ExerciseId_Should_UseBase()
        {
            var exercise = new WorkoutRoutineExercise() { ExerciseId = 1 };

            exercise.ExerciseId.Should().Be(1);
        }

        [TestMethod]
        public void WorkoutRoutineExercise_ExerciseId_Should_UseReference()
        {
            var exercise = new WorkoutRoutineExercise()
            {
                ExerciseId = 1,
                Exercise = Design.One<Exercise>().Build()
            };

            exercise.ExerciseId.Should().Be(exercise.Exercise.ExerciseId);
        }

        #endregion

        #region Collection Tests

        [TestMethod]
        public void WorkoutRoutineExerciseCollection_UpdateReferences_Should_AssignRoutine()
        {
            //  arrange
            var routine = Design.One<WorkoutRoutine>()
                .Build();

            routine.AllExercises = Design.Many<Exercise>(1)
                .All()
                .With(x => x.Group = Design.One<ExerciseGroup>().Build())
                .AsCollection();

            var exercise = routine.AllExercises.First();

            var routineExercise = Design.One<WorkoutRoutineExercise>()
                .With(x => x.ExerciseId = exercise.ExerciseId)
                .Build();

            //  act
            routine.RoutineExercises.Add(routineExercise);

            //  assert
            routineExercise.Routine.Should().BeSameAs(routine);

            routineExercise.Exercise.Should().BeSameAs(exercise);
        }

        #endregion
    }
}
