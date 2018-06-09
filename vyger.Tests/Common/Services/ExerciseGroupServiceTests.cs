using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Repositories;
using vyger.Common.Services;

namespace vyger.Tests.Common.Services
{
    [TestClass]
    public class ExerciseGroupServiceTests : BaseServiceTests<ExerciseGroupService>
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
        public void ExerciseGroupService_GetExerciseGroups_Should_SelectMany()
        {
            //	arrange
            var groups = Design.Many<ExerciseGroup>().Build();

            Moxy.GetMock<IExerciseGroupRepository>()
                .Setup(x => x.SelectMany())
                .Returns(groups);

            //	act
            var actual = SubjectUnderTest.GetExerciseGroups();

            //	assert
            actual.Count.Should().Be(groups.Count);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void ExerciseGroupService_GetExerciseGroupByPrimaryKey_Should_UsePrimaryKey()
        {
            //	arrange
            var group = Design.One<ExerciseGroup>().Build();

            Moxy.GetMock<IExerciseGroupRepository>()
                .Setup(x => x.SelectOne(group.GroupId))
                .Returns(group);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.View, group));

            //	act
            var actual = SubjectUnderTest.GetExerciseGroup(group.GroupId);

            //	assert
            actual.Should().BeSameAs(group);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Save

        [TestMethod]
        public void ExerciseGroupService_Save_Should_CreateNew()
        {
            //	arrange
            var created = Design.One<ExerciseGroup>().Build();

            Moxy.GetMock<IExerciseGroupRepository>()
                .Setup(x => x.Save(created));

            Moxy.GetMock<IExerciseGroupRepository>()
                .Setup(x => x.SelectOne(created.GroupId))
                .Returns(null as ExerciseGroup);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.EnsureAudit(created));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.Update, created));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.View, null as ExerciseGroup));

            //	act
            var actual = SubjectUnderTest.SaveExerciseGroup(created);

            //	assert
            actual.Should().BeSameAs(created);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void ExerciseGroupService_Save_Should_UpdateExisting()
        {
            //	arrange

            var original = Design.One<ExerciseGroup>().Build();

            var updated = Design.One<ExerciseGroup>().Build();

            Moxy.GetMock<IExerciseGroupRepository>()
                .Setup(x => x.Save(original));

            Moxy.GetMock<IExerciseGroupRepository>()
                .Setup(x => x.SelectOne(updated.GroupId))
                .Returns(original);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.EnsureAudit(original));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.Update, updated));

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(SecurityAccess.View, original));

            //	act
            var actual = SubjectUnderTest.SaveExerciseGroup(updated);

            //	assert
            actual.Should().BeSameAs(original);

            Moxy.VerifyAll();
        }

        #endregion
    }
}
