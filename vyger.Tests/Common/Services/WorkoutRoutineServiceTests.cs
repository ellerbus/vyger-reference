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
        public void WorkoutRoutineService_GetWorkoutRoutines_Should_CallIntoContext()
        {
            //	arrange
            var routines = Design.Many<WorkoutRoutine>()
                .All()
                .With(x => x.OwnerId = -1)
                .Build();

            routines.Last().OwnerId = 99;

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.WorkoutRoutines)
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
        public void WorkoutRoutineService_GetWorkoutRoutines_Should_CallIntoContext_AndFilter()
        {
            //	arrange
            var routines = Design.Many<WorkoutRoutine>()
                .All()
                .With(x => x.OwnerId = -1)
                .Build();

            routines.Last().OwnerId = 99;

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.WorkoutRoutines)
                .Returns(routines);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(99);

            //	act
            var actual = SubjectUnderTest.GetWorkoutRoutines();

            //	assert
            actual.Count.Should().Be(5);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void WorkoutRoutineService_GetWorkoutRoutineByPrimaryKey_Should_CallIntoContext()
        {
            //	arrange
            var access = SecurityAccess.Update;

            var routines = Design.Many<WorkoutRoutine>().Build();

            var routine = routines.Last();

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.WorkoutRoutines)
                .Returns(routines);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(access, routine));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(routine.OwnerId);

            //	act
            var actual = SubjectUnderTest.GetWorkoutRoutine(routine.RoutineId, access);

            //	assert
            actual.Should().BeSameAs(routine);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Add

        [TestMethod]
        public void WorkoutRoutineService_AddWorkoutRoutine_Should_CallContext()
        {
            //	arrange
            var routine = Design.One<WorkoutRoutine>().Build();

            var dbset = Moxy.GetMock<IDbSet<WorkoutRoutine>>();

            dbset
                .Setup(x => x.Add(routine))
                .Returns(routine);

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.WorkoutRoutines)
                .Returns(dbset.Object);

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.SaveChanges())
                .Returns(1);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(99);

            //	act
            var actual = SubjectUnderTest.AddWorkoutRoutine(routine);

            //	assert
            actual.Should().BeSameAs(routine);

            routine.OwnerId.Should().Be(99);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Save

        [TestMethod]
        public void WorkoutRoutineService_SaveChange_Should_CallContext()
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
