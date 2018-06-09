using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Collections;
using vyger.Common.Models;
using vyger.Common.Repositories;
using vyger.Common.Services;

namespace vyger.Tests.Common.Services
{
    [TestClass]
    public class WorkoutRoutineExerciseServiceTests : BaseServiceTests<WorkoutRoutineExerciseService>
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
        public void WorkoutRoutineExerciseService_GetWorkoutRoutine_Should_GetTheWorkoutRoutineHeader()
        {
            //	arrange
            var routine = Design.One<WorkoutRoutine>().Build();

            Moxy.GetMock<IWorkoutRoutineService>()
                .Setup(x => x.GetWorkoutRoutine(routine.RoutineId))
                .Returns(routine);

            //	act
            var actual = SubjectUnderTest.GetWorkoutRoutine(routine.RoutineId);

            //	assert
            routine.Should().Be(actual);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void WorkoutRoutineExerciseService_GetWorkoutRoutineExerciseByPrimaryKey_Should_UsePrimaryKey()
        {
            //	arrange
            var exercise = Design.One<WorkoutRoutineExercise>().Build();

            Moxy.GetMock<IWorkoutRoutineExerciseRepository>()
                .Setup(x => x.SelectOne(exercise.RoutineId, exercise.WeekId, exercise.DayId, exercise.ExerciseId))
                .Returns(exercise);

            //	act
            var actual = SubjectUnderTest.GetWorkoutRoutineExercise(exercise.RoutineId, exercise.WeekId, exercise.DayId, exercise.ExerciseId);

            //	assert
            actual.Should().BeSameAs(exercise);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Save

        [TestMethod]
        public void WorkoutRoutineExerciseService_Save_Should_CreateNew()
        {
            //	arrange
            var created = Design.One<WorkoutRoutineExercise>().Build();

            Moxy.GetMock<IWorkoutRoutineExerciseRepository>()
                .Setup(x => x.Update(created));

            Moxy.GetMock<IWorkoutRoutineExerciseRepository>()
                .Setup(x => x.SelectOne(created.RoutineId, created.WeekId, created.DayId, created.ExerciseId))
                .Returns(null as WorkoutRoutineExercise);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.EnsureAudit(created));

            //	act
            var actual = SubjectUnderTest.SaveWorkoutRoutineExercise(created);

            //	assert
            actual.Should().BeSameAs(created);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void WorkoutRoutineExerciseService_Save_Should_UpdateExisting()
        {
            //	arrange

            var original = Design.One<WorkoutRoutineExercise>().Build();

            var updated = Design.One<WorkoutRoutineExercise>().Build();

            Moxy.GetMock<IWorkoutRoutineExerciseRepository>()
                .Setup(x => x.Update(original));

            Moxy.GetMock<IWorkoutRoutineExerciseRepository>()
                .Setup(x => x.SelectOne(updated.RoutineId, updated.WeekId, updated.DayId, updated.ExerciseId))
                .Returns(original);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.EnsureAudit(original));

            //	act
            var actual = SubjectUnderTest.SaveWorkoutRoutineExercise(updated);

            //	assert
            actual.Should().BeSameAs(original);

            Moxy.VerifyAll();
        }

        #endregion
    }
}
