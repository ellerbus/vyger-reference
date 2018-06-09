using System.Data.Entity;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;
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
        public void ExerciseService_GetExercises_Should_CallIntoContext()
        {
            //	arrange
            var groups = Design.Many<ExerciseGroup>().Build();

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.ExerciseGroups)
                .Returns(groups);

            //	act
            var actual = SubjectUnderTest.GetExerciseGroups();

            //	assert
            actual.Count.Should().Be(groups.Count);

            Moxy.VerifyAll();
        }

        [TestMethod]
        public void ExerciseGroupService_GetExerciseGroupByPrimaryKey_Should_CallIntoContext()
        {
            //	arrange
            var access = SecurityAccess.Update;

            var groups = Design.Many<ExerciseGroup>().Build();

            var group = groups.Last();

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.ExerciseGroups)
                .Returns(groups);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.VerifyCan(access, group));

            //	act
            var actual = SubjectUnderTest.GetExerciseGroup(group.GroupId, access);

            //	assert
            actual.Should().BeSameAs(group);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Add

        [TestMethod]
        public void ExerciseGroupService_AddExerciseGroup_Should_CallContext()
        {
            //	arrange
            var group = Design.One<ExerciseGroup>().Build();

            var dbset = Moxy.GetMock<IDbSet<ExerciseGroup>>();

            dbset
                .Setup(x => x.Add(group))
                .Returns(group);

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.ExerciseGroups)
                .Returns(dbset.Object);

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.SaveChanges())
                .Returns(1);

            //	act
            var actual = SubjectUnderTest.AddExerciseGroup(group);

            //	assert
            actual.Should().BeSameAs(group);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Save

        [TestMethod]
        public void ExerciseGroupService_SaveChange_Should_CallContext()
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
