using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Repositories;
using vyger.Common.Services;

namespace vyger.Tests.Common.Services
{
    [TestClass]
    public class WorkoutRoutineServiceTests : BaseServiceTests<WorkoutRoutineService>
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
        public void WorkoutRoutineService_GetWorkoutRoutines_Should_SelectMany()
        {
            //	arrange
            var routines = Design.Many<WorkoutRoutine>().Build();

            Moxy.GetMock<IWorkoutRoutineRepository>()
                .Setup(x => x.SelectMany(99))
                .Returns(routines);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(99);

            //	act
            var actual = SubjectUnderTest.GetWorkoutRoutines();

            //	assert
            actual.Count.Should().Be(routines.Count);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void WorkoutRoutineService_GetWorkoutRoutineByPrimaryKey_Should_UsePrimaryKey()
        {
            //	arrange
            var routine = Design.One<WorkoutRoutine>().Build();

            Moxy.GetMock<IWorkoutRoutineRepository>()
                .Setup(x => x.SelectOne(routine.RoutineId))
                .Returns(routine);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.View, routine));

            //	act
            var actual = SubjectUnderTest.GetWorkoutRoutine(routine.RoutineId);

            //	assert
            actual.Should().BeSameAs(routine);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Save

        [TestMethod]
        public void WorkoutRoutineService_Save_Should_CreateNew()
        {
            //	arrange
            var created = Design.One<WorkoutRoutine>().Build();

            Moxy.GetMock<IWorkoutRoutineRepository>()
                .Setup(x => x.Save(created));

            Moxy.GetMock<IWorkoutRoutineRepository>()
                .Setup(x => x.SelectOne(created.RoutineId))
                .Returns(null as WorkoutRoutine);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.Update, created));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.View, null as WorkoutRoutine));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.EnsureAudit(created));

            //	act
            var actual = SubjectUnderTest.SaveWorkoutRoutine(created);

            //	assert
            actual.Should().BeSameAs(created);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void WorkoutRoutineService_Save_Should_UpdateExisting()
        {
            //	arrange

            var original = Design.One<WorkoutRoutine>().Build();

            var updated = Design.One<WorkoutRoutine>().Build();

            Moxy.GetMock<IWorkoutRoutineRepository>()
                .Setup(x => x.Save(original));

            Moxy.GetMock<IWorkoutRoutineRepository>()
                .Setup(x => x.SelectOne(updated.RoutineId))
                .Returns(original);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.Update, updated));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.View, original));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.EnsureAudit(original));

            //	act
            var actual = SubjectUnderTest.SaveWorkoutRoutine(updated);

            //	assert
            actual.Should().BeSameAs(original);

            Moxy.VerifyAll();
        }

        #endregion
    }
}
