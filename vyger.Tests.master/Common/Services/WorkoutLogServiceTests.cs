using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;

namespace vyger.Tests.Common.Services
{
    [TestClass]
    public class WorkoutLogServiceTests : BaseServiceTests<WorkoutLogService>
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
        public void WorkoutLogService_GetWorkoutLogs_ByDate_Should_CallIntoContext()
        {
            //	arrange
            var logs = Design.Many<WorkoutLog>()
                .WithExercises()
                .Build();

            var log = logs.Last();

            Moxy.GetMock<IVygerContext>()
                .Setup(x => x.WorkoutLogs)
                .Returns(logs);

            Moxy.GetMock<ISecurityActor>()
                .Setup(x => x.MemberId)
                .Returns(log.MemberId);

            //	act
            var actual = SubjectUnderTest.GetWorkoutLogs(log.LogDate);

            //	assert
            actual.Count.Should().Be(1);

            actual[0].Should().BeSameAs(log);

            Moxy.VerifyAll();
        }

        #endregion

        #region Tests - Save

        [TestMethod]
        public void WorkoutLogService_SaveChange_Should_CallContext()
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
