using System.Linq;
using FizzWare.NBuilder;
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
        public void ExerciseService_GetExercises_Should_SelectMany_ForOneGroup()
        {
            //	arrange
            var group = Design.One<ExerciseGroup>().Build();

            var exercises = Design.Many<Exercise>().Build();

            foreach (var exercise in exercises)
            {
                exercise.GroupId = group.GroupId;
            }

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(9);

            Moxy.GetMock<IExerciseRepository>()
                .Setup(x => x.SelectMany(1, 9))
                .Returns(exercises);

            Moxy.GetMock<IExerciseGroupService>()
                .Setup(x => x.GetExerciseGroup(1))
                .Returns(group);

            //	act
            var actual = SubjectUnderTest.GetExercises(1);

            //	assert
            actual.Count.Should().Be(exercises.Count);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void ExerciseService_GetExercises_Should_SelectMany_ForAllGroups()
        {
            //	arrange
            var exercises = Design.Many<Exercise>().Build();

            var sources = exercises.Select(x =>
            {
                return Design.One<ExerciseGroup>()
                    .With(w => w.GroupId = x.GroupId)
                    .Build();
            });

            var groups = new ExerciseGroupCollection(sources);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(9);

            Moxy.GetMock<IExerciseRepository>()
                .Setup(x => x.SelectMany(0, 9))
                .Returns(exercises);

            Moxy.GetMock<IExerciseGroupService>()
                .Setup(x => x.GetExerciseGroups())
                .Returns(groups);

            //	act
            var actual = SubjectUnderTest.GetExercises(0);

            //	assert
            actual.Count.Should().Be(exercises.Count);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void ExerciseService_GetExerciseByPrimaryKey_Should_UsePrimaryKey()
        {
            //	arrange
            var exercise = Design.One<Exercise>().Build();

            Moxy.GetMock<IExerciseRepository>()
                .Setup(x => x.SelectOne(exercise.ExerciseId))
                .Returns(exercise);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.View, exercise));

            //	act
            var actual = SubjectUnderTest.GetExercise(exercise.ExerciseId);

            //	assert
            actual.Should().BeSameAs(exercise);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Save

        [TestMethod]
        public void ExerciseService_Save_Should_CreateNew()
        {
            //	arrange
            var created = Design.One<Exercise>().Build();

            Moxy.GetMock<IExerciseRepository>()
                .Setup(x => x.Save(created));

            Moxy.GetMock<IExerciseRepository>()
                .Setup(x => x.SelectOne(created.ExerciseId))
                .Returns(null as Exercise);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.Update, created));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.View, null as Exercise));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.EnsureAudit(created));

            //	act
            var actual = SubjectUnderTest.SaveExercise(created);

            //	assert
            actual.Should().BeSameAs(created);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void ExerciseService_Save_Should_UpdateExisting()
        {
            //	arrange

            var original = Design.One<Exercise>().Build();

            var updated = Design.One<Exercise>().Build();

            Moxy.GetMock<IExerciseRepository>()
                .Setup(x => x.Save(original));

            Moxy.GetMock<IExerciseRepository>()
                .Setup(x => x.SelectOne(updated.ExerciseId))
                .Returns(original);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.Update, updated));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.View, original));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.EnsureAudit(original));

            //	act
            var actual = SubjectUnderTest.SaveExercise(updated);

            //	assert
            actual.Should().BeSameAs(original);

            Moxy.VerifyAll();
        }

        #endregion
    }
}
