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
    public class ExerciseServiceTests : BaseServiceTests<ExerciseService>
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
        public void ExerciseService_GetExercises_Should_CallIntoContext()
        {
            //	arrange
            var exercises = Design.Many<Exercise>()
                .All()
                .With(x => x.OwnerId = -1)
                .Build();

            exercises.Last().OwnerId = 99;

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.Exercises)
                .Returns(exercises);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(99);

            //	act
            var actual = SubjectUnderTest.GetExercises(0);

            //	assert
            actual.Count.Should().Be(exercises.Count);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void ExerciseService_GetExercises_Should_CallIntoContext_AndFilter()
        {
            //	arrange
            var exercises = Design.Many<Exercise>()
                .All()
                .With(x => x.OwnerId = -1)
                .Build();

            exercises.Last().OwnerId = 99;

            var groupId = exercises.Last().GroupId;

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.Exercises)
                .Returns(exercises);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(99);

            //	act
            var actual = SubjectUnderTest.GetExercises(groupId);

            //	assert
            actual.Count.Should().Be(1);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void ExerciseService_GetExerciseByPrimaryKey_Should_CallIntoContext()
        {
            //	arrange
            var access = SecurityAccess.Update;

            var exercises = Design.Many<Exercise>().Build();

            var exercise = exercises.Last();

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.Exercises)
                .Returns(exercises);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(access, exercise));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(exercise.OwnerId);

            //	act
            var actual = SubjectUnderTest.GetExercise(exercise.ExerciseId, access);

            //	assert
            actual.Should().BeSameAs(exercise);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Add

        [TestMethod]
        public void ExerciseService_AddExercise_Should_CallContext()
        {
            //	arrange
            var exercise = Design.One<Exercise>().Build();

            var dbset = Moxy.GetMock<IDbSet<Exercise>>();

            dbset
                .Setup(x => x.Add(exercise))
                .Returns(exercise);

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.Exercises)
                .Returns(dbset.Object);

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.SaveChanges())
                .Returns(1);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(99);

            //	act
            var actual = SubjectUnderTest.AddExercise(exercise);

            //	assert
            actual.Should().BeSameAs(exercise);

            exercise.OwnerId.Should().Be(99);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Save

        [TestMethod]
        public void ExerciseService_SaveChange_Should_CallContext()
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
